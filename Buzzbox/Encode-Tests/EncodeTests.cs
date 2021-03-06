﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Buzzbox_Common;
using Buzzbox_Common.Encoders;

namespace Buzzbox.Tests
{
    [TestClass()]
    public class EncodeTests
    {
        Card testMinion = new Card
        {
            Name = "Tirion Fordring",
            Type = "MINION",
            CardClass = "PALADIN",
            Rarity = "LEGENDARY",
            Cost = 8,
            Attack = 6,
            Health = 6,
            Text = "<b>Divine Shield</b>. <b>Taunt</b>. <b>Deathrattle:</b> Equip a 5/3 Ashbringer."
        };

        Card testSpell = new Card
        {
            Name = "Fireball",
            Type = "SPELL",
            CardClass = "MAGE",
            Rarity = "FREE",
            Cost = 4,
            Text = "Deal $6 damage."
        };

        Card testWeapon = new Card
        {
            Name = "Death's Bite",
            Type = "WEAPON",
            CardClass = "WARRIOR",
            Rarity = "COMMON",
            Cost = 4,
            Attack = 4,
            Durability = 2,
            Text = "<b>Deathrattle:</b> Deal 1 damage to all minions."
        };

        Card testUngoro = new Card
        {
            Name = "Awaken the Makers",
            Type = "SPELL",
            CardClass = "PRIEST",
            Rarity = "LEGENDARY",
            Cost = 1,
            Text = "<b>Quest:</b> Summon\\n7 <b>Deathrattle</b> minions.<b>\\nReward:</b> Amara, Warden of Hope."
        };

        private Card testHero = new Card
        {
            Name = "Uther of the Ebon Blade",
            Type = "HERO",
            CardClass = "PALADIN",
            Rarity = "LEGENDARY",
            Cost = 9,
            Health = 30,
            Text = "<b>Battlecry</b>: Equip a 5/3 Lifesteal weapon."
        };

        [TestMethod]
        public void EncodeCardCollectionscfdivineFormatTest()
        {
            var collection = new CardCollection();
            collection.Cards.Add(testMinion);
            collection.Cards.Add(testSpell);
            collection.Cards.Add(testWeapon);

            var encoder = new Encode();
            var result = encoder.EncodeCardCollection(collection, EncodingFormats.scfdivineFormat);

            var expected =
                "Tirion Fordring @ Paladin |  | Minion | L | 8 | 6/6 || $DV$. $T$. $DR$: Equip a 5/3 Ashbringer. &\n\nFireball @ Mage | Spell | C | 4 || Deal $6 damage. &\n\nDeath's Bite @ Warrior | Weapon | C | 4 | 4/2 || $DR$: Deal 1 damage to all minions. &\n\n";
            Assert.AreEqual(expected,result);
        }

        [TestMethod]
        public void EncodeCardCollectionMtgEncodeFormatTest()
        {
            var collection = new CardCollection();
            collection.Cards.Add(testMinion);
            collection.Cards.Add(testSpell);
            collection.Cards.Add(testWeapon);

            var encoder = new Encode();
            var result = encoder.EncodeCardCollection(collection, EncodingFormats.MtgEncoderFormat);

            var expected =
                "|3minion|4paladin|5none|6legendary|7&^^^^^^^^|8&^^^^^^|9&^^^^^^|2$DV$. $T$. $DR$: equip a &^^^^^/&^^^ ashbringer.|1tirion fordring|\n\n|3spell|4mage|6common|7&^^^^|2deal $&^^^^^^ damage.|1fireball|\n\n|3weapon|4warrior|6common|7&^^^^|8&^^^^|9&^^|2$DR$: deal &^ damage to all minions.|1death's bite|\n\n";

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EncodeUnGoroCard()
        {
            var collection = new CardCollection();
            collection.Cards.Add(testUngoro);
            
            var encoder = new Encode();
            var result = encoder.EncodeCardCollection(collection, EncodingFormats.MtgEncoderFormat);

            var expected = "|3spell|4priest|6legendary|7&^|2$QU$: summon\\n7 $DR$ minions.\\nreward: amara, warden of hope.|1awaken the makers|\n\n";

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EncodescfdivineFormatHero()
        {
            var collection = new CardCollection();
            collection.Cards.Add(testHero);

            var encoder = new Encode();
            var result = encoder.EncodeCardCollection(collection, EncodingFormats.scfdivineFormat);

            var expected =
                "Uther of the Ebon Blade @ Paladin | Hero | L | 9 | 5 || $B$: Equip a 5/3 $LS$ weapon. &\n\n";

            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        public void EncodesMtgFormatHero()
        {
            var collection = new CardCollection();
            collection.Cards.Add(testHero);

            var encoder = new Encode();
            var result = encoder.EncodeCardCollection(collection, EncodingFormats.MtgEncoderFormat);

            var expected = "|3hero|4paladin|6legendary|7&^^^^^^^^^|9&^^^^^|2$B$: equip a &^^^^^/&^^^ $LS$ weapon.|1uther of the ebon blade|\n\n";

            Assert.AreEqual(expected, result);
        }
    }
}