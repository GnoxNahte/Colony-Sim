using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using System;

namespace GnoxNahte
{
    public static class Extensions
    {
        public static ulong GenerateHash(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            SHA256 sha256 = SHA256.Create();
            byte[] hashValue = sha256.ComputeHash(bytes);
            return Convert.ToUInt64(hashValue);
        }
    }
}