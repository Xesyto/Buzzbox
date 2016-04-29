﻿using System.Collections.Generic;


namespace Buzzbox_Common
{
    class Card
    {
        public int Health { get; set; }
        public int Attack { get; set; }
        public string Artist { get; set; }
        public List<string> Mechanics { get; set; }
        public string Rarity { get; set; }
        public string Set { get; set; }
        public string HowToEarnGolden { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public string Texture { get; set; }
        public string Id { get; set; }
        public bool Collectible { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public string HowToEarn { get; set; }
        public string Flavor { get; set; }
        public List<int?> Dust { get; set; }
        public string PlayerClass { get; set; }
        public int? Durability { get; set; }
        public string Source { get; set; }
    }
}