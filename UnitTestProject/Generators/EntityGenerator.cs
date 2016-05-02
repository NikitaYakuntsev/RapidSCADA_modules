using Entity;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestProject.Generators
{
    public class EntityGenerator
    {
        private static Random rnd = new Random();

        public static Data GetData()
        {
            Data res = new Data();
            res.Name = "GeneratedData" + RandomIntModifier();
            res.Timestamp = RandomIntModifier();
            res.Value = RandomIntModifier();
            return res;
        }

        public static Device GetDevice()
        {
            Device res = new Device();
            res.Name = "GeneratedDevice" + RandomIntModifier();
            res.Working = RandomBoolModifier();
            return res;
        }

        public static Command GetCommand()
        {
            Command res = new Command();
            res.Name = "GeneratedCommand" + RandomIntModifier();
            res.Text = "command_text_" + RandomIntModifier();
            return res;
        }

        public static CommandLog GetCommandLog()
        {
            CommandLog res = new CommandLog();
            res.Name = "GeneratedCommandLog" + RandomIntModifier();
            res.Sent = RandomBoolModifier();
            res.Timestamp = RandomIntModifier();
            return res;
        }

        public static Token GetToken()
        {
            Token res = new Token();
            res.CreationDate = RandomIntModifier();
            res.ExpirationDate = RandomIntModifier();
            res.Name = "GeneratedToken" + RandomIntModifier();
            return res;
        }

        public static SystemParameter GetSystemParameter()
        {
            SystemParameter sp = new SystemParameter();
            sp.Key = "SystemParameter" + RandomIntModifier() + RandomIntModifier();
            sp.Value = RandomIntModifier().ToString();
            return sp;
        }

        private static int RandomIntModifier()
        {
            return rnd.Next(0, 100000);
        }

        private static bool RandomBoolModifier()
        {
            return rnd.Next() % 2 == 0;
        }
    }
}
