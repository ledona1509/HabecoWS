using System.Web.Services;
using Controller;
using Model;

[WebService(Namespace = "http://tempuri.org/", Description = "Habeco Webservice", Name = "HabecoWS")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service : WebService
{
    private Worker _worker;

    public Service()
    {

        //Uncomment the following line if using designed components 
        InitializeComponent();
    }

    /// <summary>
    /// Initialize worker component
    /// </summary>
    private void InitializeComponent()
    {
        _worker = new Worker();
    }

    /// <summary>
    /// Setup new connection to Habeco Server
    /// </summary>
    /// <param name="clientIp">Client Ip</param>
    /// <returns>ResponseSessionKey</returns>
    [WebMethod]
    public ResponseSessionKey SetupNewConnection(string clientIp)
    {
        return _worker.SetConnection(clientIp);
    }

    /// <summary>
    /// Get debit total
    /// </summary>
    /// <param name="dFrom">From date format dd/MM/yyyy</param>
    /// <param name="dTo">To date format dd/MM/yyyy</param>
    /// <param name="cUnit">Branch code</param>
    /// <param name="cCust">Customer code</param>
    /// <param name="sessionKey">Session key</param>
    /// <returns>ResponseDebitTotal object</returns>
    [WebMethod]
    public ResponseDebitTotal GetDebitTotal(string dFrom, string dTo, string cUnit, string cCust, string sessionKey)
    {
        return _worker.GetDebitTotal(dFrom, dTo, cUnit, cCust, sessionKey);
    }

    /// <summary>
    /// Get stock total
    /// </summary>
    /// <param name="dFrom">From date format dd/MM/yyyy</param>
    /// <param name="dTo">To date format dd/MM/yyyy</param>
    /// <param name="stite">Warehouse Code</param>
    /// <param name="cUnit">Branch code</param>
    /// <param name="sessionKey">Session key</param>
    /// <returns>ResponseStockTotal</returns>
    [WebMethod]
    public ResponseStockTotal GetStockTotal(string dFrom, string dTo, string stite, string cUnit, string sessionKey)
    {
        return _worker.GetStockTotal(dFrom, dTo, stite, cUnit, sessionKey);
    }

    /// <summary>
    /// For test API
    /// </summary>
    /// <param name="yourName">Input your name</param>
    /// <returns>Print out your name</returns>
    [WebMethod]
    public string TestAPIForDummy(string yourName)
    {
        return _worker.TestAPIForDummy(yourName);
    }
}
