﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.Dtos.Import
{
    [XmlType("partId")]
    public class PartCarsDto
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}
