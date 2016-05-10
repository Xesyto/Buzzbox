﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Buzzbox.Encoders;
using Buzzbox_Common;

namespace Buzzbox
{
    public class Encode
    {
        public bool ShuffleFields;

        public Encode()
        {
            
        }

        //Dispatch cards to be encoded to an encoder selected by command line option. then stitch them together in a stringbuilder. 
        public string EncodeCardCollection(CardCollection cardCollection, EncodingFormats encodingFormat)
        {
            var outputBuilder = new StringBuilder();
            var encoder = InstanciateEncoder(encodingFormat);

            if (ShuffleFields && encodingFormat == EncodingFormats.scfdivineFormat)
            {
                Console.WriteLine("This format cannot be shuffled");
                ShuffleFields = false;
            }

            foreach (var card in cardCollection.Cards)
            {
                var cardOutput = encoder.EncodeCard(card);

                if (ShuffleFields)
                {
                    cardOutput = ShuffleCardFields(cardOutput);
                }
                
                outputBuilder.Append($"{cardOutput}\n\n");
            }

            return outputBuilder.ToString();
        }

        private string ShuffleCardFields(string cardLine)
        {
            cardLine = cardLine.TrimEnd('|').TrimStart('|');
            List<string> fields = cardLine.Split('|').ToList();
            fields.Shuffle();
            return $"|{string.Join("|",fields)}|";
        }

        //Return an encoder based on the requested encodingFormat
        public IEncoderInterface InstanciateEncoder(EncodingFormats encodingFormat)
        {
            switch (encodingFormat)
            {
                case EncodingFormats.scfdivineFormat:
                    return new scfdivineFormatEncoder();

                case EncodingFormats.MtgFormat:
                default:
                    return new MtgEncodeFormatEncoder();
            }
        }
    }
}
