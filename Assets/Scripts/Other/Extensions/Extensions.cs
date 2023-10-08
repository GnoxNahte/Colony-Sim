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

        //Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        //{
        //    float u = 1 - t;
        //    float tt = t * t;
        //    float uu = u * u;
        //    float uuu = uu * u;
        //    float ttt = tt * t;

        //    Vector3 p = uuu * p0;
        //    p += 3 * uu * t * p1;
        //    p += 3 * u * tt * p2;
        //    p += ttt * p3;

        //    return p;
        //}
    }
}