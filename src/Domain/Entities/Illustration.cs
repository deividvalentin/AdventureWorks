﻿using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Illustration
    {
        public int IllustrationId { get; set; }
        public string Diagram { get; set; }
        public DateTime ModifiedDate { get; set; }

        public  ICollection<ProductModelIllustration> ProductModelIllustration { get; set; }
    }
}
