using Entity;
using EntityDTO.ModelsDTO;
using EntityService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityService.Facade
{
    public class DataFacade
    {
        private static DataRepository dataRep = DataRepository.GetInstance();

        public List<DataDTO> GetDatasByDevice(int deviceId, object dateFrom, object dateTo)
        {
            ICollection<Data> datas = dataRep.GetByDevice(deviceId, dateFrom, dateTo);
            List<DataDTO> res = new List<DataDTO>();
            foreach (Data d in datas)
                res.Add(DataDTO.Convert(d));

            res.Sort((data1, data2) => data2.Timestamp.CompareTo(data1.Timestamp)); 
            return res;
        }

        private DataFacade() { }
        private static DataFacade instance = new DataFacade();
        public static DataFacade GetInstance()
        {
            if (instance == null)
                instance = new DataFacade();
            return instance;
        }
    }
}
