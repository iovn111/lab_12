﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLab10
{
    public class IdNumber
    {
        int id;

        public int Id
        {
            get => id;
            set
            {
                if (value < 0)
                {
                    throw new Exception("Номер не может быть меньше 0");
                }
                id = value;
            }
        }

        public IdNumber(int number = 0)
        {
            Id = number;
        }
        public override string ToString()
        {
            return Id.ToString();
        }
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj == this) return true;
            if (obj is IdNumber)
            {
                return Id == ((IdNumber)obj).Id;
            }
            else
            {
                return false;
            }
        }
    }
}