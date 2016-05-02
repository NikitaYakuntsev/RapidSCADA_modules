using Entity.Models;
using EntityService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityService.Service
{
    public class SystemPropertyService
    {
        private static SystemParameterRepository spr = SystemParameterRepository.GetInstance();

        public bool IsRegistered()
        {
            SystemParameter reg = spr.GetByKey(Common.Dictionary.SystemProperties.SP_REGISTERED);
            return reg != null && reg.Value.ToLower().Equals("true"); 
        }
        
        public void SetValue(string key, object value)
        {
            SystemParameter sp = spr.GetByKey(key);
            if (sp == null)
            {
                sp = new SystemParameter();
                sp.Key = key;
                sp.Value = value.ToString();
                spr.Save(sp);
            }
            else
            {
                sp.Value = value.ToString();
                spr.Update(sp.Id, sp);
            }
        }

        public String GetValue(string key)
        {
            SystemParameter sp = spr.GetByKey(key);
            if (sp == null)
                return null;
            return sp.Value;
        }


        private SystemPropertyService() { }
        private static SystemPropertyService instance;
        public static SystemPropertyService GetInstance()
        {
            if (instance == null)
                instance = new SystemPropertyService();
            return instance;
        }
    }
}
