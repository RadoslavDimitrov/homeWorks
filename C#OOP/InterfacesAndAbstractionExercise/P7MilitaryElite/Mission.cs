﻿using System;
using System.Collections.Generic;
using System.Text;

namespace P7MilitaryElite
{
    public class Mission : IMission
    {
        private List<string> stateList = new List<string>()
        {
            "inprogress",
            "finished"
        };
        private string state;

        public string State
        {
            get { return state; }
            set 
            {
                if (!stateList.Contains(value.ToLower()))
                {
                    throw new ArgumentException("Invalid Mission State");
                }

                state = value; 
            }
        }

        public string CodeName { get; set; }

        public override string ToString()
        {
            return $"Code Name: {this.CodeName} State: {this.state}";
        }
    }
}
