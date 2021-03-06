﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Buzzbox_Common;

namespace Buzzbox_Decode.Decoders.Tests
{
    [TestClass()]
    public class MtgEncodeFormatDecoderTests
    {
        private Card testMinion = new Card
        {
            Name = "Tirion Fordring",
            Type = "MINION",
            CardClass = "PALADIN",
            Rarity = "LEGENDARY",
            Cost = 8,
            Attack = 6,
            Health = 6,
            Text = "<b>Divine Shield</b>. <b>Taunt</b>. <b>Deathrattle</b>: Equip a 5/3 Ashbringer."
        };

        private Card testBlankMinion = new Card
        {
            Name = "Tirion Fordring",
            Type = "MINION",
            CardClass = "PALADIN",
            Rarity = "LEGENDARY",
            Cost = 8,
            Attack = 6,
            Health = 6,
            Text = ""
        };

        private Card testSpell = new Card
        {
            Name = "Fireball",
            Type = "SPELL",
            CardClass = "MAGE",
            Rarity = "COMMON",
            Cost = 4,
            Text = "Deal $6 damage."
        };

        private Card testWeapon = new Card
        {
            Name = "Death's Bite",
            Type = "WEAPON",
            CardClass = "WARRIOR",
            Rarity = "COMMON",
            Cost = 4,
            Attack = 4,
            Durability = 2,
            Text = "<b>Deathrattle</b>: Deal 1 damage to all minions."
        };

        [TestMethod()]
        public void DecodeBlankMinionCardTest()
        {
            var decoder = new MtgEncodeFormatDecoder();
            var input =
                "|3minion|4paladin|5none|6legendary|7&^^^^^^^^|8&^^^^^^|9&^^^^^^|2|1tirion fordring|";

            var output = decoder.DecodeCard(input);

            Assert.AreEqual(testBlankMinion.Name, output.Name);
            Assert.AreEqual(testBlankMinion.Type, output.Type);
            Assert.AreEqual(testBlankMinion.CardClass, output.CardClass);
            Assert.AreEqual(testBlankMinion.Rarity, output.Rarity);
            Assert.AreEqual(testBlankMinion.Cost, output.Cost);
            Assert.AreEqual(testBlankMinion.Attack, output.Attack);
            Assert.AreEqual(testBlankMinion.Health, output.Health);
            Assert.AreEqual(testBlankMinion.Text, output.Text); //Exception for Ashbringer because detecting Proper Nouns is rather hard.

        }

        [TestMethod()]
        public void DecodeMinionCardTest()
        {
            var decoder = new MtgEncodeFormatDecoder();
            var input =
                "|3minion|4paladin|5none|6legendary|7&^^^^^^^^|8&^^^^^^|9&^^^^^^|2$DV$. $T$. $DR$: equip a &^^^^^/&^^^ ashbringer.|1tirion fordring|";

            var output = decoder.DecodeCard(input);

            Assert.AreEqual(testMinion.Name, output.Name);
            Assert.AreEqual(testMinion.Type, output.Type);
            Assert.AreEqual(testMinion.CardClass, output.CardClass);
            Assert.AreEqual(testMinion.Rarity, output.Rarity);
            Assert.AreEqual(testMinion.Cost, output.Cost);
            Assert.AreEqual(testMinion.Attack, output.Attack);
            Assert.AreEqual(testMinion.Health, output.Health);
            Assert.AreEqual("<b>Divine Shield</b>. <b>Taunt</b>. <b>Deathrattle</b>: Equip a 5/3 ashbringer.", output.Text); //Exception for Ashbringer because detecting Proper Nouns is rather hard.

        }

        [TestMethod()]
        public void DecodeSpellCardTest()
        {
            var decoder = new MtgEncodeFormatDecoder();
            var input = "|3spell|4mage|6common|7&^^^^|2deal $&^^^^^^ damage.|1fireball|";

            var output = decoder.DecodeCard(input);

            Assert.AreEqual(testSpell.Name, output.Name);
            Assert.AreEqual(testSpell.Type, output.Type);
            Assert.AreEqual(testSpell.CardClass, output.CardClass);
            Assert.AreEqual(testSpell.Rarity, output.Rarity);
            Assert.AreEqual(testSpell.Cost, output.Cost);
            Assert.AreEqual(testSpell.Text, output.Text);
        }

        [TestMethod()]
        public void DecodeWeaponCardTest()
        {
            var decoder = new MtgEncodeFormatDecoder();
            var input = "|3weapon|4warrior|6common|7&^^^^|8&^^^^|9&^^|2$DR$: deal &^ damage to all minions.|1death's bite|";

            var output = decoder.DecodeCard(input);

            Assert.AreEqual(testWeapon.Name, output.Name);
            Assert.AreEqual(testWeapon.Type, output.Type);
            Assert.AreEqual(testWeapon.CardClass, output.CardClass);
            Assert.AreEqual(testWeapon.Rarity, output.Rarity);
            Assert.AreEqual(testWeapon.Cost, output.Cost);
            Assert.AreEqual(testWeapon.Attack, output.Attack);
            Assert.AreEqual(testWeapon.Durability, output.Durability);
            Assert.AreEqual(testWeapon.Text, output.Text);
        }

    }
}