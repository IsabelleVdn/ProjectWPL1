﻿using System;
using System.Collections.Generic;

namespace TestingPlanner.Domain.Models
{
    public partial class TestDevision
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}