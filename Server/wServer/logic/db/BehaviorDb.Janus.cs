﻿using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ Janus = () => Behav()
        //entity used to change states 
        .Init("md dwGenerator",
            new State(
                new State("nokillswag",
                    new ConditionalEffect(ConditionEffectIndex.Invincible)
                    )
                )
            )
        .Init("md LightKey",
            new State(
                new State("Bullet15",
                    new Shoot(10, count: 15, projectileIndex: 0, coolDown: 800),
                    new EntityExistsTransition("md dwGenerator", 9999, "MoveToJanus")
                    ),
                new State("MoveToJanus",
                    new MoveTo(0, 6, speed: 0.5, isMapPosition: false, once: true),
                    new TimedTransition(2000, "Bullet8")
                    ),
                new State("Bullet8",
                    new Shoot(10, count: 8, projectileIndex: 1, coolDown: new Cooldown(1500, 500)),
                    new SpecificHeal(20, 2000, "Janus", coolDown: 9999),
                    new EntitiesNotExistsTransition(9999, "MoveAwayJanus", "md dwGenerator")
                    ),
                new State("MoveAwayJanus",
                    new MoveTo(0, -6, speed: 0.5, isMapPosition: false, once: true),
                    new TimedTransition(2000, "Bullet15")
                    )
                )
            )
        .Init("md DarkKey",
            new State(
                new State("Bullet15",
                    new Shoot(10, count: 15, projectileIndex: 0, coolDown: 800),
                    new EntityExistsTransition("md dwGenerator", 9999, "MoveToJanus")
                    ),
                new State("MoveToJanus",
                    new MoveTo(6, 6, speed: 0.5, isMapPosition: false, once: true),
                    new TimedTransition(2000, "Bullet8")
                    ),
                new State("Bullet8",
                    new Shoot(10, count: 8, projectileIndex: 1, coolDown: new Cooldown(1500, 500)),
                    new SpecificHeal(20, 2000, "Janus", coolDown: 9999),
                    new EntitiesNotExistsTransition(9999, "MoveAwayJanus", "md dwGenerator")
                    ),
                new State("MoveAwayJanus",
                    new MoveTo(-6, -6, speed: 0.5, isMapPosition: false, once: true),
                    new TimedTransition(2000, "Bullet15")
                    )
                )
            )
        .Init("md Janus the Doorwarden",
                new State(
                    new HpLessTransition(0.15, "ragetime"),
                    new State("idle",
                        new PlayerWithinTransition(8, "taunt")
                        ),
                    new State("taunt",
                        new Flash(0xFF0000, 2, 2),
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Taunt("You bare witness to Janus, Doorwarden of Oryx. You will soon regret your decisions, your soul sealed away -- forever"),
                        new TimedTransition(5000, "tosskeys")
                        ),
                    new State("tosskeys",
                        new Taunt(1.00, "Keys, protect your master"),
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new TossObject("md LightKey", 8, 0, coolDown: 9999999, randomToss: false),
                        new TossObject("md DarkKey", 10, 45, coolDown: 9999999, randomToss: false),
                        new TossObject("md LightKey", 8, 90, coolDown: 9999999, randomToss: false),
                        new TossObject("md DarkKey", 10, 135, coolDown: 9999999, randomToss: false),
                        new TossObject("md LightKey", 8, 180, coolDown: 9999999, randomToss: false),
                        new TossObject("md DarkKey", 10, 225, coolDown: 9999999, randomToss: false),
                        new TossObject("md LightKey", 8, 270, coolDown: 9999999, randomToss: false),
                        new TossObject("md DarkKey", 10, 315, coolDown: 9999999, randomToss: false),
                        new TimedTransition(2000, "ringattacks")
                        ),
                    new State(
                        new EntitiesNotExistsTransition(9999, "warn1", "md DarkKey","md LightKey"),
                    new State("ringattacks",
                        new RemoveEntity(9999, "md dwGenerator"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Shoot(10, 10, projectileIndex: 0, coolDown: 100),                 
                        new TimedTransition(10000, "keyshelp")
                        ),
                    new State("keyshelp",
                        new Spawn("md dwGenerator", 1, 1, coolDown: 999999),
                        new TimedTransition(11000, "ringattacks")
                        )
                       ),
                    new State("warn1",
                        new Flash(0xFF0000, 1, 2),
                        new Taunt(1.00, "Up and Down!"),
                        new TimedTransition(3000, "UpDown")
                        ),
                    new State("UpDown",
                        new Shoot(10, count: 8, fixedAngle: 0, shootAngle: 6, projectileIndex: 1, coolDown: 50),
                        new Shoot(10, count: 8, fixedAngle: 180, shootAngle: 6, projectileIndex: 1, coolDown: 50),
                        new TimedTransition(5000, "warn2")
                        ),
                    new State("warn2",
                        new Flash(0xFF0000, 1, 2),
                        new Taunt(1.00, "Left and Right!"),
                        new TimedTransition(3000, "LeftRight")
                        ),
                     new State("LeftRight",
                        new Shoot(10, count: 8, fixedAngle: 90, shootAngle: 6, projectileIndex: 1, coolDown: 50),
                        new Shoot(10, count: 8, fixedAngle: 270, shootAngle: 6, projectileIndex: 1, coolDown: 50),
                        new TimedTransition(5000, "warn3")
                        ),
                     new State("warn3",
                        new Flash(0xFF0000, 1, 2),
                        new Taunt(1.00, "Quick! Up and Down!"),
                        new TimedTransition(3000, "UpDown2")
                        ),
                     new State("UpDown2",
                        new Shoot(10, count: 8, fixedAngle: 0, shootAngle: 6, projectileIndex: 1, coolDown: 50),
                        new Shoot(10, count: 8, fixedAngle: 180, shootAngle: 6, projectileIndex: 1, coolDown: 50),
                        new TimedTransition(5000, "warn4")
                        ),
                     new State("warn4",
                        new Flash(0xFF0000, 1, 2),
                        new Taunt(1.00, "Hurry! Left and Right!"),
                        new TimedTransition(3000, "LeftRight2")
                        ),
                     new State("LeftRight2",
                        new Shoot(10, count: 8, fixedAngle: 90, shootAngle: 6, projectileIndex: 1, coolDown: 50),
                        new Shoot(10, count: 8, fixedAngle: 270, shootAngle: 6, projectileIndex: 1, coolDown: 50),
                        new TimedTransition(5000, "tosskeys")
                        ),
                     //spookyrage weird movement
                     new State("ragetime",
                      new State("1",
                        new Prioritize(
                             new Wander(0.4),
                             new Follow(0.6, 8, 1)
                            ),
                        new Shoot(10, 10, projectileIndex: 0, coolDown: new Cooldown(200, 200)),
                        new TimedTransition(6000, "2")
                        ),
                      new State("2",
                        new StayBack(1, 6),
                        new Shoot(10, 10, projectileIndex: 0, coolDown: new Cooldown(100, 100)),
                        new TimedTransition(6000, "1")
                        )
                      )
                    ),
                new MostDamagers(3,
                    new ItemLoot("Potion of Vitality", 1.0)
                ),
                new MostDamagers(1,
                    new ItemLoot("Potion of Speed", 1.0)
                ),
                new Threshold(0.02,
                    new TierLoot(9, ItemType.Weapon, 0.1),
                    new TierLoot(5, ItemType.Ability, 0.1),
                    new TierLoot(9, ItemType.Armor, 0.1),
                    new TierLoot(7, ItemType.Armor, 0.05),
                    new TierLoot(10, ItemType.Weapon, 0.05),
                    new TierLoot(5, ItemType.Ring, 0.025),
                    new ItemLoot("Wine Cellar Incantation", 0.01)
                )
            )


        ;
    }
}