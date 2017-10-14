using wServer.logic.behaviors;
using wServer.logic.transitions;
using wServer.logic.loot;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ GameKeeperRealm = () => Behav()
        .Init("Keeper Minion Rise",
            new State(
                new State("0",
                    new SetAltTexture(0),
                    new EntityNotExistsTransition("Keeper Minion Activator", 50000, "1")
                    ),
                new State("1",
                    new SetAltTexture(1),
                    new TimedTransition(200, "2")
                    ),
                new State("2",
                    new SetAltTexture(2),
                    new TimedTransition(200, "3")
                    ),
                new State("3",
                    new SetAltTexture(3),
                    new TimedTransition(200, "4")
                    ),
                new State("4",
                    new SetAltTexture(4),
                    new TimedTransition(200, "5")
                    ),
                new State("5",
                    new SetAltTexture(5),
                    new TimedTransition(200, "6")
                    ),
                new State("6",
                    new SetAltTexture(6),
                    new TimedTransition(200, "7")
                    ),
                new State("7",
                    new SetAltTexture(7),
                    new TimedTransition(200, "8")
                    ),
                new State("8",
                    new SetAltTexture(8),
                    new TimedTransition(200, "9")
                    ),
                new State("9",
                    new SetAltTexture(9),
                    new TimedTransition(200, "10")
                    ),
                new State("10",
                    new SetAltTexture(10),
                    new TimedTransition(200, "11")
                    ),
                new State("11",
                    new SetAltTexture(11),
                    new TimedTransition(200, "12")
                    ),
                new State("12",
                    new SetAltTexture(12),
                    new TimedTransition(200, "13")
                    ),
                new State("13",
                    new SetAltTexture(13),
                    new TimedTransition(10, "spawn")
                    ),
                new State("spawn",
                    new Spawn("Keeper Minion", maxChildren: 1, initialSpawn: 0.5),
                    new Suicide()
                    )
                )
            )
            .Init("Keeper Minion",
            new State(
                new State("begin",
                     new Follow(1.5, range: 0.2),
                     new Shoot(100, projectileIndex: 0, count: 1, coolDown: 75)
                    )
                )
            )
            .Init("Keeper Minion Activator",
            new State(
                new State("begin",
                     new TimedTransition(5000, "talk")
                    ),
                new State("talk",
                    new Taunt(true, "Bored without anything to ruin? I�ll give you some excitement."),
                    new TimedTransition(1000, "die")
                    ),
                new State("die",
                    new Suicide()
                    )
                )
            )
            .Init("Keeper Defender",
                new State(
                    new State("begin"
                        ),
                    new State("die",
                        new Suicide()
                    )
                    )
            )
        .Init("Keeper Attacker",
                new State(
                    new State("begin"
                        ),
                    new State("die",
                        new Suicide()
                    )
                    )
            )
        .Init("Keeper Support",
                new State(
                    new State("begin"
                        ),
                    new State("die",
                        new Suicide()
                    )
                    )
            )
        .Init("Keeper Boss Anchor",
                new State(
                    new State("begin",
                        new Spawn("Keeper Gilgor Boss", maxChildren: 1)
                        )
                    )
            )
            .Init("Keeper Gods Activator",
            new State(
                new State("begin",
                    new EntitiesNotExistsTransition(999, "talk", "Keeper Minion Activator", "Keeper Minion", "Keeper Minion Rise")
                    ),
                new State("talk",
                    new Taunt(true, "You survived my minions? No problem, they were the weakest of the bunch!"),
                    new TimedTransition(2000, "talk1")
                    ),
                new State("talk1",
                    new Taunt(true, "I AM GOD! LET THERE BE LIGHT!"),
                    new ApplySetpiece("KeeperGodland"),
                    new TimedTransition(2000, "talk2")
                    ),
                new State("talk2",
                    new Taunt(true, "Let�s see what you can do against the gods you have killed!"),
                    new EntitiesNotExistsTransition(999, "godsDead", "beholder", "ent god", "leviathan", "White Demon", "Slime God", "native sprite god", "medusa", "ghost god")
                    ),
                new State("godsDead",
                    new Taunt(true, "Fine, you may have killed my gods, but you are forgetting one thing..."),
                    new TimedTransition(2000, "godsDead2")
                    ),
                new State("godsDead2",
                    new Taunt(true, "THIS REALM IS MINE"),
                    new TimedTransition(4000, "KeeperClose")
                    ),
                new State("KeeperClose",
                    new ApplySetpiece("KeeperClose"),
                    new TimedTransition(3700, "KeeperClose1")
                    ),
                new State("KeeperClose1",
                    new ApplySetpiece("KeeperClose1"),
                    new TimedTransition(3000, "KeeperClose2")
                    ),
                new State("KeeperClose2",
                    new ApplySetpiece("KeeperClose2"),
                    new TimedTransition(3000, "KeeperClose3")
                    ),
                new State("KeeperClose3",
                    new ApplySetpiece("KeeperClose3"),
                    new TimedTransition(3000, "Tomb")
                    ),
                new State("Tomb",
                    new ApplySetpiece("KeeperTomb")
                    ),
                new State("Shatters",
                    new ApplySetpiece("KeeperShatters"),
                    new Taunt(true, "Remember this king? He's Nothing compared to me")
                    )
                )
            )
        .Init("Keeper Gilgor Boss Appear",
                new State(
                    new State("init",
                        new TimedTransition(700, "die")
                        ),
                    new State("die",
                        new Suicide()
                        )
                    )
            )
        .Init("Keeper Gilgor Boss Disappear",
                new State(
                    new State("init",
                        new TimedTransition(700, "die")
                        ),
                    new State("die",
                        new Suicide()
                        )
                    )
            )

        .Init("Keeper Crown",
                new State(
                    new State("begin",
                        new TimedTransition(5000, "die")
                        ),
                    new State("die",
                        new Suicide()
                    )
                    )
            )

        .Init("Pillar of Gilgor",
                new State(
                    new EntityNotExistsTransition("Keeper Gilgor Boss", 999, "die"),
                    new State("begin",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                        new Flash(0xffffff, 0.5, 6)
                        ),
                    new State("Shoot",
                        new Shoot(99, 1, coolDown: 3000, coolDownOffset: 1000)
                    ),
                    new State("blowthemup",
                        new TossObject("shtrs Firebomb", 1, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 2, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 3, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 4, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 5, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 6, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 6, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 5, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 4, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 3, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 2, randomToss: true, coolDown: 5000)
                    ),
                    new State("die",
                        new Suicide()
                    )
                    )
            )

        .Init("Pillar of Gilgor2",
                new State(
                    new EntityNotExistsTransition("Keeper Gilgor Boss", 999, "die"),
                    new State("begin",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                        new Flash(0xffffff, 0.5, 6),
                        new TimedTransition(3000, "Shoot")
                        ),
                    new State("Shoot",
                        new Shoot(99, 1, coolDown: 3000, coolDownOffset: 1000)
                    ),
                    new State("blowthemup",
                        new TossObject("shtrs Firebomb", 1, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 2, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 3, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 4, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 5, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 6, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 6, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 5, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 4, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 3, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 2, randomToss: true, coolDown: 5000)
                    ),
                    new State("die",
                        new Suicide()
                    )
                    )
            )

        .Init("Minion of Gilgor",
                new State(
                    new State("init"),
                    new State("begin",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, false),
                        new Flash(0xffffff, 0.5, 6),
                        new TimedTransition(3000, "gettem")
                        ),
                    new State("gettem",
                        new Follow(1, 99, 1),
                        new Shoot(99, 1, coolDown: 500)
                    ),
                    new State("blowthemup",
                        new TossObject("shtrs Firebomb", 1, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 2, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 3, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 4, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 5, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 6, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 6, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 5, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 4, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 3, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 2, randomToss: true, coolDown: 5000),
                        new TossObject("shtrs Firebomb", 1, randomToss: true, coolDown: 5000)   
                    )
                    )
            )

        .Init("Keeper Gilgor Boss",
                new State(
                    new State("init",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, perm: true),
                        new Spawn("Keeper Gilgor Boss Appear", maxChildren: 1),
                        new TimedTransition(800, "sneakIn0")
                        ),
                    new State("sneakIn0",
                        new SetAltTexture(1),
                        new Orbit(2, 19, target: "Keeper Boss Anchor", acquireRange: 30),
                        new TimedTransition(3000, "sneakIn1")
                        ),
                    new State("sneakIn1",
                        new Orbit(2, 18, target: "Keeper Boss Anchor", acquireRange: 30),
                        new TimedTransition(3000, "sneakIn2")
                        ),
                    new State("sneakIn2",
                        new Orbit(2, 17, target: "Keeper Boss Anchor", acquireRange: 30),
                        new TimedTransition(3000, "sneakIn3")
                        ),
                    new State("sneakIn3",
                        new Orbit(2, 16, target: "Keeper Boss Anchor", acquireRange: 30),
                        new TimedTransition(3000, "fadeOut1")
                        ),
                    new State("fadeOut1",
                        new SetAltTexture(2),
                        new Spawn("Keeper Gilgor Boss Disappear", maxChildren: 1),
                        new TimedTransition(800, "moveToSpawn1")
                        ),
                    new State("moveToSpawn1",
                        new ReturnToSpawn(true, 0.9),
                        new TimedTransition(4000, "moveToTomb")
                        ),
                    new State("moveToTomb",
                        new MoveTo(8, 9, speed: 1, isMapPosition: false, once: true, instant: false),
                        new TimedTransition(50, "fadeIn1")
                        ),
                    new State("fadeIn1",
                        new Spawn("Keeper Gilgor Boss Appear", maxChildren: 1),
                        new TimedTransition(700, "fadeIn2")
                        ),
                    new State("fadeIn2",
                        new SetAltTexture(1),
                        new TimedTransition(3000, "shootTomb1")
                        ),
                    new State("shootTomb1",
                        new Taunt(true, "Say goodbye to your precious Gods!"),
                        new Shoot(100, count: 1, projectileIndex: 0, shootAngle: 200, fixedAngle: 200, coolDown: 10000000),
                        new TimedTransition(500, "kill1")
                        ),
                    new State("kill1",
                        new Order(999, "Keeper Attacker", "die"),
                        new SetAltTexture(2),
                        new Spawn("Keeper Gilgor Boss Disappear", maxChildren: 1),
                        new TimedTransition(100, "moveToTomb2")
                        ),
                    new State("moveToTomb2",
                        new MoveTo(-2, -5),
                        new TimedTransition(500, "fadeIn3")
                        ),
                    new State("fadeIn3",
                        new Spawn("Keeper Gilgor Boss Appear", maxChildren: 1),
                        new TimedTransition(700, "fadeIn4")
                        ),
                    new State("fadeIn4",
                        new SetAltTexture(1),
                        new TimedTransition(200, "shootTomb2")
                        ),
                    new State("shootTomb2",
                        new Shoot(100, count: 1, projectileIndex: 0, shootAngle: 200, fixedAngle: 200, coolDown: 10000000),
                        new TimedTransition(500, "kill2")
                        ),
                    new State("kill2",
                        new Order(999, "Keeper Defender", "die"),
                        new SetAltTexture(2),
                        new Spawn("Keeper Gilgor Boss Disappear", maxChildren: 1),
                        new TimedTransition(100, "moveToTomb3")
                        ),
                    new State("moveToTomb3",
                        new MoveTo(5, 3),
                        new TimedTransition(500, "fadeIn5")
                        ),
                    new State("fadeIn5",
                        new Spawn("Keeper Gilgor Boss Appear", maxChildren: 1),
                        new TimedTransition(700, "fadeIn6")
                        ),
                    new State("fadeIn6",
                        new SetAltTexture(1),
                        new TimedTransition(200, "shootTomb3")
                        ),
                    new State("shootTomb3",
                        new Shoot(100, count: 1, projectileIndex: 0, shootAngle: 200, fixedAngle: 200, coolDown: 10000000),
                        new TimedTransition(200, "kill3")
                        ),
                    new State("kill3",
                        new Order(999, "Keeper Support", "die"),
                        new Taunt(true, "By killing the gracious ones who provide life, your offspring will be weakened!"),
                        new TimedTransition(2500, "disappear")
                        ),
                    new State("disappear",
                        new SetAltTexture(2),
                        new Spawn("Keeper Gilgor Boss Disappear", maxChildren: 1),
                        new TimedTransition(500, "initShatts")
                        ),
                     new State("initShatts",
                        new Order(999, "Keeper Gods Activator", "Shatters"),
                        new TimedTransition(1000, "wait")
                        ),
                     new State("wait",
                         new TimedTransition(4000, "fadeIn7")
                         ),
                     new State("fadeIn7",
                        new Spawn("Keeper Gilgor Boss Appear", maxChildren: 1),
                        new TimedTransition(700, "fadeIn8")
                        ),
                    new State("fadeIn8",
                        new SetAltTexture(1),
                        new TimedTransition(200, "LetsDoThis2")
                        ),
                    new State("LetsDoThis2",
                        new ReturnToSpawn(true, 0.9),
                        new Taunt(true, "I summon my pillars from hell to take you to their creator"),
                        new TimedTransition(1000, "ItsHappening")
                        ),
                    new State("ItsHappening",
                        new TossObject("Pillar of Gilgor", 8, 0, coolDown: 1000000),
                        new TossObject("Pillar of Gilgor", 8, 180, coolDown: 1000000),
                        new ConditionalEffect(ConditionEffectIndex.Invincible, false),
                        new TimedTransition(3000, "ShootThePillars1")
                            ),
                    new State("ShootThePillars1",
                        new Taunt(true, "FIRE!"),
                        new Order(99, "Pillar Of Gilgor", "Shoot"),
                        new HpLessTransition(0.9, "getAngery")
                        ),
                    new State("getAngery",
                        new SetAltTexture(3),
                        new ConditionalEffect(ConditionEffectIndex.Invincible, false),
                        new Taunt(true, "MINIONS, STOP THEM!"),
                        new TossObject("Minion of Gilgor", 3, 0, coolDown: 1000000),
                        new TossObject("Minion of Gilgor", 3, 45, coolDown: 1000000, coolDownOffset: 200),
                        new TossObject("Minion of Gilgor", 3, 90, coolDown: 1000000, coolDownOffset: 400),
                        new TossObject("Minion of Gilgor", 3, 135, coolDown: 1000000, coolDownOffset: 600),
                        new TossObject("Minion of Gilgor", 3, 180, coolDown: 1000000, coolDownOffset: 800),
                        new TossObject("Minion of Gilgor", 3, 225, coolDown: 1000000, coolDownOffset: 1000),
                        new TossObject("Minion of Gilgor", 3, 270, coolDown: 1000000, coolDownOffset: 1200),
                        new TossObject("Minion of Gilgor", 3, 315, coolDown: 1000000, coolDownOffset: 1400),
                        new TimedTransition(2800, "startMinions")
                        ),
                    new State("startMinions", 
                        new Order(999, "Minion of Gilgor", "begin"),
                        new TimedTransition(1, "getouttathere")
                        ),
                    new State("getouttathere",
                        new HpLessTransition(0.8, "getAngery2")
                        ),
                    new State("getAngery2",
                        new Taunt(true, "You. Will. Stop."),
                        new TossObject("Pillar of Gilgor2", 8, 90, coolDown: 1000000),
                        new TossObject("Pillar of Gilgor2", 8, 270, coolDown: 1000000),
                        new HpLessTransition(0.6, "blowthemtoshreds")
                        ),
                    new State("blowthemtoshreds",
                        new SetAltTexture(4),
                        new Taunt(true, "I think I'll have to borrow some things from you, king"),
                        new Order(999, "Pillar of Gilgor", "blowthemup"),
                        new Order(999, "Pillar of Gilgor2", "blowthemup"),
                        new TimedTransition(3000, "goback")
                        ),
                    new State("goback",
                        new SetAltTexture(3),
                        new HpLessTransition(0.4, "rage")
                        ),
                    new State("rage",
                        new Taunt(true, "ENOUGH"),
                        new Order(999, "Pillar of Gilgor", "die"),
                        new Order(999, "Pillar of Gilgor2", "die"),
                        new StayCloseToSpawn(0.75, 3),
                        new Shoot(99, 1, projectileIndex: 1, shootAngle: 0, fixedAngle: 0, coolDown: 1800),
                        new Shoot(99, 1, projectileIndex: 1, shootAngle: 45, fixedAngle: 45, coolDown: 1800, coolDownOffset: 200),
                        new Shoot(99, 1, projectileIndex: 1, shootAngle: 90, fixedAngle: 90, coolDown: 1800, coolDownOffset: 400),
                        new Shoot(99, 1, projectileIndex: 1, shootAngle: 135, fixedAngle: 135, coolDown: 1800, coolDownOffset: 600),
                        new Shoot(99, 1, projectileIndex: 1, shootAngle: 180, fixedAngle: 180, coolDown: 1800, coolDownOffset: 800),
                        new Shoot(99, 1, projectileIndex: 1, shootAngle: 225, fixedAngle: 225, coolDown: 1800, coolDownOffset: 1000),
                        new Shoot(99, 1, projectileIndex: 1, shootAngle: 270, fixedAngle: 270, coolDown: 1800, coolDownOffset: 1200),
                        new Shoot(99, 1, projectileIndex: 1, shootAngle: 315, fixedAngle: 315, coolDown: 1800, coolDownOffset: 1400),
                        new TossObject("Minion of Gilgor", 3, 0, coolDown: 7000),
                        new TossObject("Minion of Gilgor", 3, 45, coolDown: 7000, coolDownOffset: 200),
                        new TossObject("Minion of Gilgor", 3, 90, coolDown: 7000, coolDownOffset: 400),
                        new TossObject("Minion of Gilgor", 3, 135, coolDown: 7000, coolDownOffset: 600),
                        new TossObject("Minion of Gilgor", 3, 180, coolDown: 7000, coolDownOffset: 800),
                        new TossObject("Minion of Gilgor", 3, 225, coolDown: 7000, coolDownOffset: 1000),
                        new TossObject("Minion of Gilgor", 3, 270, coolDown: 7000, coolDownOffset: 1200),
                        new TossObject("Minion of Gilgor", 3, 315, coolDown: 7000, coolDownOffset: 1400),
                        new HpLessTransition(0.2, "fakeit")
                        ),
                    new State("fakeit",
                        new ReturnToSpawn(true, 0.5),
                        new Flash(0xffffff, 0.5, 3),
                        new Taunt("Hero, You have done the impossible, and you shall be rewarded with..."),
                        new TimedTransition(2000, "prankit")
                    ),
                    new State("prankit",
                        new SetAltTexture(5),
                        new Taunt(true, "A SWIFT DEATH!!!"),
                        new TimedTransition(2000, "ragerage")
                        ),
                    new State("ragerage",
                        new SetAltTexture(3),
                        new TossObject("Minion of Gilgor", 3, 0, coolDown: 1000000),
                        new TossObject("Minion of Gilgor", 3, 45, coolDown: 1000000, coolDownOffset: 200),
                        new TossObject("Minion of Gilgor", 3, 90, coolDown: 1000000, coolDownOffset: 400),
                        new TossObject("Minion of Gilgor", 3, 135, coolDown: 1000000, coolDownOffset: 600),
                        new TossObject("Minion of Gilgor", 3, 180, coolDown: 1000000, coolDownOffset: 800),
                        new TossObject("Minion of Gilgor", 3, 225, coolDown: 1000000, coolDownOffset: 1000),
                        new TossObject("Minion of Gilgor", 3, 270, coolDown: 1000000, coolDownOffset: 1200),
                        new TossObject("Minion of Gilgor", 3, 315, coolDown: 1000000, coolDownOffset: 1400),
                        new TossObject("Pillar of Gilgor", 8, 0, coolDown: 1000000),
                        new TossObject("Pillar of Gilgor", 8, 180, coolDown: 1000000),
                        new TossObject("Pillar of Gilgor2", 8, 90, coolDown: 1000000),
                        new TossObject("Pillar of Gilgor2", 8, 270, coolDown: 1000000),
                        new TimedTransition(3000, "BegintheChaos")
                        ),
                    new State("BegintheChaos",
                        new Order(999, "Pillar of Gilgor", "blowthemup"),
                        new Order(999, "Pillar of Gilgor2", "blowthemup"),
                        new Order(999, "Minion of Gilgor", "begin"),
                        new TimedTransition(1, "KeepBegin")
                        ),
                    new State("KeepBegin",
                        new Shoot(99, 1, projectileIndex: 2, shootAngle: 0, fixedAngle: 0, coolDown: 1800),
                        new Shoot(99, 1, projectileIndex: 2, shootAngle: 45, fixedAngle: 45, coolDown: 1800, coolDownOffset: 200),
                        new Shoot(99, 1, projectileIndex: 2, shootAngle: 90, fixedAngle: 90, coolDown: 1800, coolDownOffset: 400),
                        new Shoot(99, 1, projectileIndex: 2, shootAngle: 135, fixedAngle: 135, coolDown: 1800, coolDownOffset: 600),
                        new Shoot(99, 1, projectileIndex: 2, shootAngle: 180, fixedAngle: 180, coolDown: 1800, coolDownOffset: 800),
                        new Shoot(99, 1, projectileIndex: 2, shootAngle: 225, fixedAngle: 225, coolDown: 1800, coolDownOffset: 1000),
                        new Shoot(99, 1, projectileIndex: 2, shootAngle: 270, fixedAngle: 270, coolDown: 1800, coolDownOffset: 1200),
                        new Shoot(99, 1, projectileIndex: 2, shootAngle: 315, fixedAngle: 315, coolDown: 1800, coolDownOffset: 1400)
                        )
            ),
            new Threshold(0.025,
                    new TierLoot(12, ItemType.Weapon, 0.3),
                    new TierLoot(6, ItemType.Ability, 0.3),
                    new TierLoot(7, ItemType.Ability, 0.1),
                    new TierLoot(13, ItemType.Armor, 0.3),
                    new TierLoot(6, ItemType.Ring, 0.3),
                    new TierLoot(7, ItemType.Ring, 0.2),
                    new TierLoot(14, ItemType.Armor, 0.2),
                    new TierLoot(13, ItemType.Weapon, 0.2),
                    new ItemLoot("Tainted Treasure Shovel", 0.3),
                    new ItemLoot("Potion of Life", 0.5),
                    new ItemLoot("Potion of Mana", 0.5),
                    new ItemLoot("Sword of the Realm", 0.01),
                    new ItemLoot("Helm of Unadulterated Evil", 0.01),
                    new ItemLoot("Armor Of Gilgor", 0.01),
                    new ItemLoot("Amulet of the Gods", 0.01)
                )
            )
        ;
    }
}
