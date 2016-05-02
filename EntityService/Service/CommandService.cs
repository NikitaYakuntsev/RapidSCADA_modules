using Common;
using Entity;
using EntityService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityService.Service
{
    public class CommandService
    {
        private static CommandRepository commRep = CommandRepository.GetInstance();
        private static CommandLogRepository commLogRep = CommandLogRepository.GetInstance();
        private static TokenRepository tokRep = TokenRepository.GetInstance();

        private CommandService() { }
        private static CommandService instance = new CommandService();

        public static CommandService GetInstance()
        {
            if (instance == null)
                instance = new CommandService();
            return instance;
        }

        public bool AddCommandToQueue(int commandId, int tokenId)
        {
            Command recieved = commRep.GetById(commandId);
            CommandLog newRecord = new CommandLog();
            newRecord.Command = recieved;
            newRecord.Name = recieved.Name;
            newRecord.Sent = false;
            newRecord.Timestamp = Utils.GetUnixTime();
            Token token = tokRep.GetById(tokenId);
            newRecord.Token = token;
            commLogRep.Save(newRecord);
            return true;
        }

    }
}
