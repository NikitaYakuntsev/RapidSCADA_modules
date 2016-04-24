using Entity;
using System;
using System.Collections.Generic;
using EntityService.Repository;
using System.Linq;
using System.Text;

namespace UnitTestProject.Generators
{
    public class ScenarioGenerator
    {
        private static DeviceRepository devRep = DeviceRepository.GetInstance();
        private static DataRepository dataRep = DataRepository.GetInstance();
        private static CommandRepository commRep = CommandRepository.GetInstance();
        private static CommandLogRepository commLogRep = CommandLogRepository.GetInstance();
        private static TokenRepository tokRep = TokenRepository.GetInstance();

        public static Data GetFilledData(Device device = null)
        {
            Data data = EntityGenerator.GetData();
            if (device == null)
                device = GetFilledDevice();
            data.Device = device;
            dataRep.Save(data);
            return data;
        }

        public static Command GetFilledCommand(Device device = null)
        {
            Command comm = EntityGenerator.GetCommand();
            if (device == null)
                device = GetFilledDevice();
            comm.Device = device;
            commRep.Save(comm);
            return comm;
        }

        public static CommandLog GetFilledCommandLog(Command comm = null, Token tok = null)
        {
            CommandLog comL = EntityGenerator.GetCommandLog();
            if (comm == null)
                comm = GetFilledCommand();
            if (tok == null)
                tok = GetFilledToken();
            comL.Command = comm;
            comL.Token = tok;
            commLogRep.Save(comL);
            return comL;
        }

        public static Device GetFilledDevice()
        {
            Device device = EntityGenerator.GetDevice();
            devRep.Save(ref device);
            return device;
        }

        public static Token GetFilledToken()
        {
            Token token = EntityGenerator.GetToken();
            tokRep.Save(token);
            return token;
        }
    }
}
