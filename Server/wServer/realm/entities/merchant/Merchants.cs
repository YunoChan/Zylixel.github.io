﻿#region

using System;
using System.Collections.Generic;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using wServer.networking.svrPackets;
using wServer.realm.entities.player;
using System.Linq;

#endregion

namespace wServer.realm.entities.merchant
{
    public class Merchants : SellableObject
    {
        private const int BuyNoFame = 6;
        private const int BuyNoGold = 9;
        private const int MerchantSize = 100;
        private const int ClothPrice = 50;

        private bool _playerMarket;
        private int _tickcount;
        private int _itemChange;

        public static Random Random { get; private set; }
          
        public Merchants(RealmManager manager, ushort objType, int itemChangeLocal = -1, World owner = null)
            : base(manager, objType)
        {
            Size = MerchantSize;
            if (owner != null)
                Owner = owner;

            _itemChange = itemChangeLocal;

            if (Random == null) Random = new Random();
            if (owner != null) ResolveMType();
        }
        
        public int MType { get; set; }
        public int MTime { get; set; }
        public int MRemaining { get; set; }
        public int Discount { get; set; }

        protected override void ExportStats(IDictionary<StatsType, object> stats)
        {
            stats[StatsType.MerchantMerchandiseType] = MType;
            stats[StatsType.MerchantRemainingCount] = MRemaining;
            stats[StatsType.MerchantRemainingMinute] = MTime;
            stats[StatsType.MerchantDiscount] = Discount;
            stats[StatsType.SellablePrice] = Price;
            stats[StatsType.SellableRankRequirement] = RankReq;
            stats[StatsType.SellablePriceCurrency] = Currency;

            base.ExportStats(stats);
        }

        public override void Init(World owner)
        {
            base.Init(owner);
            ResolveMType();
            UpdateCount++;
        }

        protected override bool TryDeduct(Player player)
        {
            if (Currency == CurrencyType.Fame)
                return player.Client.Account.Stats.Fame >= Price;
            else
                return player.Client.Account.Credits >= Price;
        }

        public override void Buy(Player player)
        {
            Manager.Database.DoActionAsync(db =>
            {
                if (ObjectType == 0x01ca) //Merchant
                {
                    if (player._buyCooldown > 0)
                    {
                        player.SendDialogError("You are buying items too fast, please slow down and re-enter the world if needed");
                        return;
                    }
                    if (TryDeduct(player))
                    {
                        for (var i = 0; i < player.Inventory.Length; i++)
                        {
                            try
                            {
                                XElement ist;
                                Manager.GameData.ObjectTypeToElement.TryGetValue((ushort)MType, out ist);
                                if (player.Inventory[i] == null &&
                                    (player.SlotTypes[i] == 10 ||
                                     player.SlotTypes[i] == Convert.ToInt16(ist.Element("SlotType").Value)))
                                {
                                    if (Currency == CurrencyType.Fame)
                                        player.CurrentFame = player.Client.Account.Stats.Fame = db.UpdateFame(player.Client.Account, -Price);
                                    else
                                        player.Credits = player.Client.Account.Credits = db.UpdateCredit(player.Client.Account, -Price);

                                    if (_playerMarket) {
                                        var item = db.GetMarketInfo(MType, Price, player.Manager.GameData);
                                        
                                        MySqlCommand cmd = db.CreateQuery();
                                        cmd.CommandText = "UPDATE stats SET fame = fame + @Price WHERE accId=@accId";
                                        cmd.Parameters.AddWithValue("@accId", item.currentUser);
                                        cmd.Parameters.AddWithValue("@Price", Price);
                                        cmd.ExecuteNonQuery();

                                        cmd.CommandText = "DELETE FROM market WHERE serialid=@id";
                                        cmd.Parameters.AddWithValue("@id", item.serialId);
                                        cmd.ExecuteNonQuery();

                                        player.Inventory[i] = item;

                                        Merchant.updatePrice(MType, Manager);
                                    }
                                    else
                                    {
                                        player.Inventory[i] = Manager.CreateSerial(Manager.GameData.Items[(ushort)MType], "Nexus Merchant");
                                    }

                                    player.Client.SendPacket(new BuyResultPacket
                                    {
                                        Result = 0,
                                        Message = "{\"key\":\"server.buy_success\"}"
                                    });
                                    player.Client.Save();
                                    player.UpdateCount++;
                                    UpdateCount++;
                                    return;
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                        player.Client.SendPacket(new BuyResultPacket
                        {
                            Result = 0,
                            Message = "{\"key\":\"server.inventory_full\"}"
                        });
                    }
                    else
                    {
                        if (Currency == CurrencyType.Fame)
                            player.Client.SendPacket(new BuyResultPacket
                            {
                                Result = BuyNoFame,
                                Message = "{\"key\":\"server.not_enough_fame\"}"
                            });
                        else
                            player.Client.SendPacket(new BuyResultPacket
                            {
                                Result = BuyNoGold,
                                Message = "{\"key\":\"server.not_enough_fame\"}"
                            });
                    }
                }
            });
        }

        public override void Tick(RealmTime time)
        {
            try
            {
                if (_playerMarket)
                {
                    if (MType == -1)
                        Owner?.LeaveWorld(this);
                    else if (MTime == -1 && Owner != null || !MerchantLists.ZyList.Contains(MType))
                    {
                        Recreate(this, false);
                        UpdateCount++;
                    }
                    else if (Price != MerchantLists.price[MType])
                    {
                        Recreate(this, true);
                        UpdateCount++;
                    }

                    _tickcount++;
                }
                if (_tickcount % (Manager?.TPS * 60) == 0) //once per minute after spawning
                {
                    MTime--;
                    UpdateCount++;
                }

                base.Tick(time);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void Recreate(Merchants x, bool keepMType)
        {
            Merchants mrc;
            if (keepMType)
                mrc = new Merchants(Manager, x.ObjectType, MType, x.Owner);
            else
                mrc = new Merchants(Manager, x.ObjectType);

            mrc.Move(x.X, x.Y);
            var w = Owner;
            Owner.LeaveWorld(this);
            w.Timers.Add(new WorldTimer(1, (world, time) => w.EnterWorld(mrc)));
        }

        public void ResolveMType()
        {
            MType = -1;
            var list = MerchantLists.ZyList;
            switch (Owner.Map[(int)X, (int)Y].Region)
            {
                case TileRegion.Store_1: list = MerchantLists.KeyList; break;
                case TileRegion.Store_12: list = MerchantLists.AccessoryDyeList; break;
                case TileRegion.Store_13: list = MerchantLists.ClothingClothList; break;
                case TileRegion.Store_14: list = MerchantLists.AccessoryClothList; break;
                case TileRegion.Store_15: list = MerchantLists.ClothingDyeList; break;
            }

            if (list == MerchantLists.ZyList)
                _playerMarket = true;

            list.Shuffle();
            foreach (var t1 in list)
            {
                if (_itemChange != -1)
                    MType = _itemChange;
                else
                    MType = t1;

                if (MType == -1)
                    Owner?.LeaveWorld(this);

                MTime = Random.Next(2, 3);

                Price = _playerMarket ? MerchantLists.price[MType] : ClothPrice;
                Currency = list == MerchantLists.KeyList ? CurrencyType.Gold : CurrencyType.Fame;
                
                UpdateCount++;
                return;
            }
        }
    }
}