﻿using System;
using IdGenerator.Obfuscat;
using IdGenerator.Snowflake;

namespace IdGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var worker = new IdWorker(2, 1);
            var idObfuscator = new IdObfuscator();
            ulong i = ulong.MaxValue;
            while (true)
            {
                ulong id = (ulong)worker.NextId();
                var feistelID = idObfuscator.Permute(id);
                var reFeistelID = idObfuscator.Permute(feistelID);
                var base62 = idObfuscator.PermuteToBase62(id);
                i--;
                Console.WriteLine($"ID:{id} , Length:{id.ToString().Length}; feistelID:{feistelID},refeistelID:{reFeistelID},base62:{base62}");
                if (id != reFeistelID) {
                    Console.WriteLine($"加解密异常:原始id:{id}, feistelID:{feistelID},refeistelID:{reFeistelID},base62:{base62}");
                    break;
                }
            }
            Console.ReadKey();
        }
    }
}
