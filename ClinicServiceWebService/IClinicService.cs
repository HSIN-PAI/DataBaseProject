using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ClinicServiceWebService
{
    // 注意: 您可以使用 [重構] 功能表上的 [重新命名] 命令同時變更程式碼和組態檔中的介面名稱 "IClinicService"。
    [ServiceContract]
    public interface IClinicService
    {
        [OperationContract]
        string HelloWorld();

        [OperationContract]
        DataTable QueryClinic(string strClinicName);

        [OperationContract]
        string QueryClinicToJson(string strClinicName);
    }
}
