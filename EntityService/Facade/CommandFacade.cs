using Entity;
using EntityService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityService.Facade
{
    public class CommandFacade
    {
        private static CommandRepository commRep = CommandRepository.GetInstance();
        private static CommandLogRepository commLogRep = CommandLogRepository.GetInstance();
        private static TokenRepository tokRep = TokenRepository.GetInstance();

        private CommandFacade() { }
        private static CommandFacade instance = new CommandFacade();

        public static CommandFacade GetInstance()
        {
            if (instance == null)
                instance = new CommandFacade();
            return instance;
        }

        public bool AddCommandToQueue(int commandId, int tokenId)
        {
            Command recieved = commRep.GetById(commandId);
            CommandLog newRecord = new CommandLog();
            newRecord.Command = recieved;
            newRecord.Name = recieved.Name;
            newRecord.Sent = false;
            newRecord.Timestamp = UnixTimeNow();
            Token token = tokRep.GetById(tokenId);
            newRecord.Token = token;
            commLogRep.Save(newRecord);
            return true;
        }

        private long UnixTimeNow()
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }
    }
}
