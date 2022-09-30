﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PocketLearn.Core.Learning
{
    public class LearnCard
    {
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public CardType CardType { get; set; }
        public DateTime LastLearnedTime { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
