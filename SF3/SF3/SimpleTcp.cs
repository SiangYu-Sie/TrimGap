using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace TcpTest
{
    class SimpleTcp
    {
        TcpListener server;			// TCPサーバーインスタンス
        TcpClient client;			// 接続後のインスタンス
        NetworkStream stream;		// インスタンス
        Socket aSocket;

        bool blnConnect = false;	// 連接狀態屬性
        bool blnServer;             // Is Server Type
        bool isServerClose = false;

        Int32 port;
        IPAddress address;


        /// <summary>
        /// 最後一個錯誤
        /// </summary>
        public string LastError { get; private set; }

        //Reconnect
        TimeoutSW to;

        readonly Log _log;
        public SimpleTcp(Log log) { _log = log; }

        void AddLog(string msg) => _log.AddLog(msg, "TCP");

        /// <summary>
        /// オープン
        /// init ソケットをOPENして待ち状態にする
        /// </summary>
        /// <returns>true:初期化成功, false:初期化失敗</returns>
        public bool Open(DeviceSetting commCond)
        {
            // TCP通信条件の設定
            blnServer = commCond.UseServer;
            port = commCond.TcpPort;
            address = IPAddress.Parse(commCond.Address);

            try
            {
                LastError = string.Empty;
                // 關閉連線
                if (blnConnect) Close();

                // 新增Log
                string message = string.Format("Socket：Open.({0}, Port={1}, blnServer={2})",
                                            address, port, blnServer);
                AddLog(message);

                recvBuf.Clear();

                // Server
                if (blnServer)
                {
                    if (server != null)
                    {
                        server.Stop();
                        server = null;
                    }

                    server = new TcpListener(address, port);
                    server.Start();
                    server.BeginAcceptTcpClient(
                        new AsyncCallback(DoAcceptTcpClientCallback),
                        server);
                }
                // Client
                else
                {
                    if (client != null)
                    {
                        this.client.Close();
                        client = null;
                    }
                    client = new TcpClient();
                    client.Connect(address, port);
                    aSocket = client.Client;
                    stream = client.GetStream();

                    if (client.Connected)
                    {
                        blnConnect = true;
                        AddLog("(Reconnect) Socket：Success Connect to Server.");
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                LastError = "ConnectError";
                // 新增Log
                AddLog("Socket：Open Exception Error=" + ex.Message);
                return false;
            }
        }

        public bool Close()
        {
            bool ret = true;
            blnConnect = false;
            try
            {
                LastError = string.Empty;

                // 新增Log
                AddLog("Socket：Close.");

                if (blnServer)
                {
                    isServerClose = true;
                    server.Stop();
                }
                if (stream != null) stream.Close();
                if (client != null) client.Close();
            }
            catch (Exception ex)
            {
                LastError = "ConnectError";
                // 新增Log
                AddLog("Socket：Close Exception Error=" + ex.Message);
                ret = false;
            }

            stream = null;
            client = null;
            server = null;
            return ret;
        }


        #region Winsock Client Method

        public bool Send(byte[] sendData)
        {
            LastError = string.Empty;

            try
            {
                // 新增Log
                string message = string.Format("Socket：Send ={0}", Encoding.ASCII.GetString(sendData));
                AddLog(message);

                if (client != null)
                {
                    NetworkStream serverStream = client.GetStream();
                    stream = client.GetStream();
                    //send cmd
                    stream.Write(sendData, 0, sendData.Length);
                }
                return true;
            }
            catch (Exception ex)
            {
                LastError = "SendError";
                // 新增Log
                AddLog("Socket：Send Exception Error =" + ex.Message);
                return false;
            }
        }


        List<byte> recvBuf = new List<byte>();
        byte[] readBuf = new byte[8192];

        void CheckRecvBuffer()
        {
            if (aSocket == null) return;            // 將連接狀態設置為已連接
            if (!IsSocketConnected(aSocket)) return;

            try
            {
                if (!stream.CanRead) return;
                if (!stream.DataAvailable) return;

                int readCont = stream.Read(readBuf, 0, readBuf.Length);
                recvBuf.AddRange(readBuf.Take(readCont));
            }
            catch (SocketException ex)
            {
                AddLog(string.Format("Socket：Monitor Receive Error={0} {1} {2}.", getErrMsg(ex.ErrorCode), ex.Message, ex.StackTrace));
            }
            catch (Exception ex)
            {
                AddLog(string.Format("Socket：Monitor Receive Error={0} {1}.", ex.Message, ex.StackTrace));
            }
        }

        public void ClearRecvBuffer() => recvBuf.Clear();

        bool IsSocketConnected(Socket s)
        {
            if (s == null) return false;
            if (s.Connected == false) return false;
            bool part1 = s.Poll(100, SelectMode.SelectRead);
            bool part2 = (s.Available == 0);
            if ((part1 && part2) || !s.Connected)
                return false;
            else
                return true;
        }

        // Process the client connection.
        void DoAcceptTcpClientCallback(IAsyncResult ar)
        {
            if (isServerClose) return;
            // Get the server that handles the client request.
            // the console.
            try
            {
                TcpListener mServer = ar.AsyncState as TcpListener;
                client = mServer.EndAcceptTcpClient(ar);
                NetworkStream networkStream = client.GetStream();
                stream = networkStream;
                // Process the connection here. (Add the client to a
                // server table, read data, etc.)
                aSocket = client.Client;

                AddLog("Socket：Success Connected by Server.");
                blnConnect = true;
                // continue server
                mServer.BeginAcceptTcpClient(new AsyncCallback(DoAcceptTcpClientCallback), mServer);
            }
            catch (Exception ex)
            {
                AddLog("Socket：Server Connect Exception Error=" + ex.Message);
            }
        }

        public byte[] Recv( bool blnAscii)
        {
            TimeoutSW aTimer = new TimeoutSW(1);
            aTimer.Start();
            while (true)
            {
                if (aTimer.IsTimeout)
                {
                    AddLog("Socket Receive Is Timeout");
                    break;
                }

                CheckRecvBuffer();

                int iCount = recvBuf.Count;
                if (iCount > 0)
                {
                    if (!blnAscii) break;
                    else if (recvBuf[iCount - 1] == Otsuka.SF3.byteETX) break;
                }

                SpinWait.SpinUntil(() => false, 20);
            }

            if (blnAscii)
            {
                return recvBuf.Skip(1).Take(recvBuf.Count - 2).ToArray();
            }
            else
                return recvBuf.ToArray();
        }
        public byte[] RecvGetResult()
        {
            TimeoutSW aTimer = new TimeoutSW(5);
            aTimer.Start();
            while (true)
            {
                if (aTimer.IsTimeout)
                {
                    AddLog("Socket Receive Is Timeout");
                    break;
                }

                CheckRecvBuffer();

                int iCount = recvBuf.Count;
                if (iCount > 0)
                {
                    if (recvBuf[0] == Otsuka.SF3.byteSOH)
                    {
                        var iDataCnt = BitConverter.ToInt32(recvBuf.Skip(1).Take(4).ToArray(), 0);
                        // SOH,Binary number 不計算
                        if (iDataCnt == (iCount - 1 - 4)) break;
                    }
                }

                SpinWait.SpinUntil(() => false, 20);
            }

            try
            {
                if (recvBuf[0] == Otsuka.SF3.byteSOH)
                    return recvBuf.Skip(1).ToArray();
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
            
            
        }

        #region Winsock ErrorCode Member
        ///<summary>A blocking operation was interrupted by a call to WSACancelBlockingCall.</summary>
        public const int WSAEINTR = 10004;
        ///<summary>The file handle supplied is not valid.</summary>
        public const int WSAEBADF = 10009;
        ///<summary>An attempt was made to access a socket in a way forbidden by its access permissions.</summary>
        public const int WSAEACCES = 10013;
        ///<summary>The system detected an invalid pointer address in attempting to use a pointer argument in a call.</summary>
        public const int WSAEFAULT = 10014;
        ///<summary>An invalid argument was supplied.</summary>
        public const int WSAEINVAL = 10022;
        ///<summary>Too many open sockets.</summary>
        public const int WSAEMFILE = 10024;
        ///<summary>A non-blocking socket operation could not be completed immediately.</summary>
        public const int WSAEWOULDBLOCK = 10035;
        ///<summary>A blocking operation is currently executing.</summary>
        public const int WSAEINPROGRESS = 10036;
        ///<summary>An operation was attempted on a non-blocking socket that already had an operation in progress.</summary>
        public const int WSAEALREADY = 10037;
        ///<summary>An operation was attempted on something that is not a socket.</summary>
        public const int WSAENOTSOCK = 10038;
        ///<summary>A required address was omitted from an operation on a socket.</summary>
        public const int WSAEDESTADDRREQ = 10039;
        ///<summary>A message sent on a datagram socket was larger than the internal message buffer or some other network limit, or the buffer used to receive a datagram into was smaller than the datagram itself.</summary>
        public const int WSAEMSGSIZE = 10040;
        ///<summary>A protocol was specified in the socket function call that does not support the semantics of the socket type requested.</summary>
        public const int WSAEPROTOTYPE = 10041;
        ///<summary>An unknown, invalid, or unsupported option or level was specified in a getsockopt or setsockopt call.</summary>
        public const int WSAENOPROTOOPT = 10042;
        ///<summary>The requested protocol has not been configured into the system, or no implementation for it exists.</summary>
        public const int WSAEPROTONOSUPPORT = 10043;
        ///<summary>The support for the specified socket type does not exist in this address family.</summary>
        public const int WSAESOCKTNOSUPPORT = 10044;
        ///<summary>The attempted operation is not supported for the type of object referenced.</summary>
        public const int WSAEOPNOTSUPP = 10045;
        ///<summary>The protocol family has not been configured into the system or no implementation for it exists.</summary>
        public const int WSAEPFNOSUPPORT = 10046;
        ///<summary>An address incompatible with the requested protocol was used.</summary>
        public const int WSAEAFNOSUPPORT = 10047;
        ///<summary>Only one usage of each socket address (protocol/network address/port) is normally permitted.</summary>
        public const int WSAEADDRINUSE = 10048;
        ///<summary>The requested address is not valid in its context.</summary>
        public const int WSAEADDRNOTAVAIL = 10049;
        ///<summary>A socket operation encountered a dead network.</summary>
        public const int WSAENETDOWN = 10050;
        ///<summary>A socket operation was attempted to an unreachable network.</summary>
        public const int WSAENETUNREACH = 10051;
        ///<summary>The connection has been broken due to keep-alive activity detecting a failure while the operation was in progress.</summary>
        public const int WSAENETRESET = 10052;
        ///<summary>An established connection was aborted by the software in your host machine.</summary>
        public const int WSAECONNABORTED = 10053;
        ///<summary>An existing connection was forcibly closed by the remote host.</summary>
        public const int WSAECONNRESET = 10054;
        ///<summary>An operation on a socket could not be performed because the system lacked sufficient buffer space or because a queue was full.</summary>
        public const int WSAENOBUFS = 10055;
        ///<summary>A connect request was made on an already connected socket.</summary>
        public const int WSAEISCONN = 10056;
        ///<summary>A request to send or receive data was disallowed because the socket is not connected and (when sending on a datagram socket using a sendto call) no address was supplied.</summary>
        public const int WSAENOTCONN = 10057;
        ///<summary>A request to send or receive data was disallowed because the socket had already been shut down in that direction with a previous shutdown call.</summary>
        public const int WSAESHUTDOWN = 10058;
        ///<summary>Too many references to some kernel object.</summary>
        public const int WSAETOOMANYREFS = 10059;
        ///<summary>A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond.</summary>
        public const int WSAETIMEDOUT = 10060;
        ///<summary>No connection could be made because the target machine actively refused it.</summary>
        public const int WSAECONNREFUSED = 10061;
        ///<summary>Cannot translate name.</summary>
        public const int WSAELOOP = 10062;
        ///<summary>Name component or name was too int.</summary>
        public const int WSAENAMETOOint = 10063;
        ///<summary>A socket operation failed because the destination host was down.</summary>
        public const int WSAEHOSTDOWN = 10064;
        ///<summary>A socket operation was attempted to an unreachable host.</summary>
        public const int WSAEHOSTUNREACH = 10065;
        ///<summary>Cannot remove a directory that is not empty.</summary>
        public const int WSAENOTEMPTY = 10066;
        ///<summary>A Windows Sockets implementation may have a limit on the number of applications that may use it simultaneously.</summary>
        public const int WSAEPROCLIM = 10067;
        ///<summary>Ran out of quota.</summary>
        public const int WSAEUSERS = 10068;
        ///<summary>Ran out of disk quota.</summary>
        public const int WSAEDQUOT = 10069;
        ///<summary>File handle reference is no inter available.</summary>
        public const int WSAESTALE = 10070;
        ///<summary>Item is not available locally.</summary>
        public const int WSAEREMOTE = 10071;
        ///<summary>WSAStartup cannot function at this time because the underlying system it uses to provide network services is currently unavailable.</summary>
        public const int WSASYSNOTREADY = 10091;
        ///<summary>The Windows Sockets version requested is not supported.</summary>
        public const int WSAVERNOTSUPPORTED = 10092;
        ///<summary>Either the application has not called WSAStartup, or WSAStartup failed.</summary>
        public const int WSANOTINITIALISED = 10093;
        ///<summary>Returned by WSARecv or WSARecvFrom to indicate the remote party has initiated a graceful shutdown sequence.</summary>
        public const int WSAEDISCON = 10101;
        ///<summary>No more results can be returned by WSALookupServiceNext.</summary>
        public const int WSAENOMORE = 10102;
        ///<summary>A call to WSALookupServiceEnd was made while this call was still processing. The call has been canceled.</summary>
        public const int WSAECANCELLED = 10103;
        ///<summary>The procedure call table is invalid.</summary>
        public const int WSAEINVALIDPROCTABLE = 10104;
        ///<summary>The requested service provider is invalid.</summary>
        public const int WSAEINVALIDPROVIDER = 10105;
        ///<summary>The requested service provider could not be loaded or initialized.</summary>
        public const int WSAEPROVIDERFAILEDINIT = 10106;
        ///<summary>A system call that should never fail has failed.</summary>
        public const int WSASYSCALLFAILURE = 10107;
        ///<summary>No such service is known. The service cannot be found in the specified name space.</summary>
        public const int WSASERVICE_NOT_FOUND = 10108;
        ///<summary>The specified class was not found.</summary>
        public const int WSATYPE_NOT_FOUND = 10109;
        ///<summary>No more results can be returned by WSALookupServiceNext.</summary>
        public const int WSA_E_NO_MORE = 10110;
        ///<summary>A call to WSALookupServiceEnd was made while this call was still processing. The call has been canceled.</summary>
        public const int WSA_E_CANCELLED = 10111;
        ///<summary>A database query failed because it was actively refused.</summary>
        public const int WSAEREFUSED = 10112;
        ///<summary>No such host is known.</summary>
        public const int WSAHOST_NOT_FOUND = 11001;
        ///<summary>This is usually a temporary error during hostname resolution and means that the local server did not receive a response from an authoritative server.</summary>
        public const int WSATRY_AGAIN = 11002;
        ///<summary>A non-recoverable error occurred during a database lookup.</summary>
        public const int WSANO_RECOVERY = 11003;
        ///<summary>The requested name is valid, but no data of the requested type was found.</summary>
        public const int WSANO_DATA = 11004;
        ///<summary>At least one reserve has arrived.</summary>
        public const int WSA_QOS_RECEIVERS = 11005;
        ///<summary>At least one path has arrived.</summary>
        public const int WSA_QOS_SENDERS = 11006;
        ///<summary>There are no senders.</summary>
        public const int WSA_QOS_NO_SENDERS = 11007;
        ///<summary>There are no receivers.</summary>
        public const int WSA_QOS_NO_RECEIVERS = 11008;
        ///<summary>Reserve has been confirmed.</summary>
        public const int WSA_QOS_REQUEST_CONFIRMED = 11009;
        ///<summary>Error due to lack of resources.</summary>
        public const int WSA_QOS_ADMISSION_FAILURE = 11010;
        ///<summary>Rejected for administrative reasons - bad credentials.</summary>
        public const int WSA_QOS_POLICY_FAILURE = 11011;
        ///<summary>Unknown or conflicting style.</summary>
        public const int WSA_QOS_BAD_STYLE = 11012;
        ///<summary>Problem with some part of the filterspec or providerspecific buffer in general.</summary>
        public const int WSA_QOS_BAD_OBJECT = 11013;
        ///<summary>Problem with some part of the flowspec.</summary>
        public const int WSA_QOS_TRAFFIC_CTRL_ERROR = 11014;
        ///<summary>General QOS error.</summary>
        public const int WSA_QOS_GENERIC_ERROR = 11015;
        ///<summary>An invalid or unrecognized service type was found in the flowspec.</summary>
        public const int WSA_QOS_ESERVICETYPE = 11016;
        ///<summary>An invalid or inconsistent flowspec was found in the QOS structure.</summary>
        public const int WSA_QOS_EFLOWSPEC = 11017;
        ///<summary>Invalid QOS provider-specific buffer.</summary>
        public const int WSA_QOS_EPROVSPECBUF = 11018;
        ///<summary>An invalid QOS filter style was used.</summary>
        public const int WSA_QOS_EFILTERSTYLE = 11019;
        ///<summary>An invalid QOS filter type was used.</summary>
        public const int WSA_QOS_EFILTERTYPE = 11020;
        ///<summary>An incorrect number of QOS FILTERSPECs were specified in the FLOWDESCRIPTOR.</summary>
        public const int WSA_QOS_EFILTERCOUNT = 11021;
        ///<summary>An object with an invalid ObjectLength field was specified in the QOS provider-specific buffer.</summary>
        public const int WSA_QOS_EOBJLENGTH = 11022;
        ///<summary>An incorrect number of flow descriptors was specified in the QOS structure.</summary>
        public const int WSA_QOS_EFLOWCOUNT = 11023;
        ///<summary>An unrecognized object was found in the QOS provider-specific buffer.</summary>
        public const int WSA_QOS_EUNKNOWNPSOBJ = 11024;
        ///<summary>An invalid policy object was found in the QOS provider-specific buffer.</summary>
        public const int WSA_QOS_EPOLICYOBJ = 11025;
        ///<summary>An invalid QOS flow descriptor was found in the flow descriptor list.</summary>
        public const int WSA_QOS_EFLOWDESC = 11026;
        ///<summary>An invalid or inconsistent flowspec was found in the QOS provider-specific buffer.</summary>
        public const int WSA_QOS_EPSFLOWSPEC = 11027;
        ///<summary>An invalid FILTERSPEC was found in the QOS provider-specific buffer.</summary>
        public const int WSA_QOS_EPSFILTERSPEC = 11028;
        ///<summary>An invalid shape discard mode object was found in the QOS provider-specific buffer.</summary>
        public const int WSA_QOS_ESDMODEOBJ = 11029;
        ///<summary>An invalid shaping rate object was found in the QOS provider-specific buffer.</summary>
        public const int WSA_QOS_ESHAPERATEOBJ = 11030;
        ///<summary>A reserved policy element was found in the QOS provider-specific buffer.</summary>
        public const int WSA_QOS_RESERVED_PETYPE = 11031;
        #endregion

        #region Winsock GetErrorCode Method

        private string getErrMsg(int errCode)
        {
            string strRet;

            switch (errCode)
            {
                case WSAEINTR:
                    strRet = "A blocking operation was interrupted by a call to WSACancelBlockingCall.";
                    break;
                case WSAEBADF:
                    strRet = "The file handle supplied is not valid.";
                    break;
                case WSAEACCES:
                    strRet = "An attempt was made to access a socket in a way forbidden by its access permissions.";
                    break;
                case WSAEFAULT:
                    strRet = "The system detected an invalid pointer address in attempting to use a pointer argument in a call.";
                    break;
                case WSAEINVAL:
                    strRet = "An invalid argument was supplied.";
                    break;
                case WSAEMFILE:
                    strRet = "Too many open sockets.";
                    break;
                case WSAEWOULDBLOCK:
                    strRet = "A non-blocking socket operation could not be completed immediately.";
                    break;
                case WSAEINPROGRESS:
                    strRet = "A blocking operation is currently executing.";
                    break;
                case WSAEALREADY:
                    strRet = "An operation was attempted on a non-blocking socket that already had an operation in progress.";
                    break;
                case WSAENOTSOCK:
                    strRet = "An operation was attempted on something that is not a socket.";
                    break;
                case WSAEDESTADDRREQ:
                    strRet = "A required address was omitted from an operation on a socket.";
                    break;
                case WSAEMSGSIZE:
                    strRet = "A message sent on a datagram socket was larger than the internal message buffer or some other network limit, or the buffer used to receive a datagram into was smaller than the datagram itself.";
                    break;
                case WSAEPROTOTYPE:
                    strRet = "A protocol was specified in the socket function call that does not support the semantics of the socket type requested.";
                    break;
                case WSAENOPROTOOPT:
                    strRet = "An unknown, invalid, or unsupported option or level was specified in a getsockopt or setsockopt call.";
                    break;
                case WSAEPROTONOSUPPORT:
                    strRet = "The requested protocol has not been configured into the system, or no implementation for it exists.";
                    break;
                case WSAESOCKTNOSUPPORT:
                    strRet = "The support for the specified socket type does not exist in this address family.";
                    break;
                case WSAEOPNOTSUPP:
                    strRet = "The attempted operation is not supported for the type of object referenced.";
                    break;
                case WSAEPFNOSUPPORT:
                    strRet = "The protocol family has not been configured into the system or no implementation for it exists.";
                    break;
                case WSAEAFNOSUPPORT:
                    strRet = "An address incompatible with the requested protocol was used.";
                    break;
                case WSAEADDRINUSE:
                    strRet = "Only one usage of each socket address (protocol/network address/port) is normally permitted.";
                    break;
                case WSAEADDRNOTAVAIL:
                    strRet = "The requested address is not valid in its context.";
                    break;
                case WSAENETDOWN:
                    strRet = "A socket operation encountered a dead network.";
                    break;
                case WSAENETUNREACH:
                    strRet = "A socket operation was attempted to an unreachable network.";
                    break;
                case WSAENETRESET:
                    strRet = "The connection has been broken due to keep-alive activity detecting a failure while the operation was in progress.";
                    break;
                case WSAECONNABORTED:
                    strRet = "An established connection was aborted by the software in your host machine.";
                    break;
                case WSAECONNRESET:
                    strRet = "An existing connection was forcibly closed by the remote host.";
                    break;
                case WSAENOBUFS:
                    strRet = "An operation on a socket could not be performed because the system lacked sufficient buffer space or because a queue was full.";
                    break;
                case WSAEISCONN:
                    strRet = "A connect request was made on an already connected socket.";
                    break;
                case WSAENOTCONN:
                    strRet = "A request to send or receive data was disallowed because the socket is not connected and (when sending on a datagram socket using a sendto call) no address was supplied.";
                    break;
                case WSAESHUTDOWN:
                    strRet = "A request to send or receive data was disallowed because the socket had already been shut down in that direction with a previous shutdown call.";
                    break;
                case WSAETOOMANYREFS:
                    strRet = "Too many references to some kernel object.";
                    break;
                case WSAETIMEDOUT:
                    strRet = "A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond.";
                    break;
                case WSAECONNREFUSED:
                    strRet = "No connection could be made because the target machine actively refused it.";
                    break;
                case WSAELOOP:
                    strRet = "Cannot translate name.";
                    break;
                case WSAENAMETOOint:
                    strRet = "Name component or name was too int.";
                    break;
                case WSAEHOSTDOWN:
                    strRet = "A socket operation failed because the destination host was down.";
                    break;
                case WSAEHOSTUNREACH:
                    strRet = "A socket operation was attempted to an unreachable host.";
                    break;
                case WSAENOTEMPTY:
                    strRet = "Cannot remove a directory that is not empty.";
                    break;
                case WSAEPROCLIM:
                    strRet = "A Windows Sockets implementation may have a limit on the number of applications that may use it simultaneously.";
                    break;
                case WSAEUSERS:
                    strRet = "Ran out of quota.";
                    break;
                case WSAEDQUOT:
                    strRet = "Ran out of disk quota.";
                    break;
                case WSAESTALE:
                    strRet = "File handle reference is no inter available.";
                    break;
                case WSAEREMOTE:
                    strRet = "Item is not available locally.";
                    break;
                case WSASYSNOTREADY:
                    strRet = "WSAStartup cannot function at this time because the underlying system it uses to provide network services is currently unavailable.";
                    break;
                case WSAVERNOTSUPPORTED:
                    strRet = "The Windows Sockets version requested is not supported.";
                    break;
                case WSANOTINITIALISED:
                    strRet = "Either the application has not called WSAStartup, or WSAStartup failed.";
                    break;
                case WSAEDISCON:
                    strRet = "Returned by WSARecv or WSARecvFrom to indicate the remote party has initiated a graceful shutdown sequence.";
                    break;
                case WSAENOMORE:
                    strRet = "No more results can be returned by WSALookupServiceNext.";
                    break;
                case WSAECANCELLED:
                    strRet = "A call to WSALookupServiceEnd was made while this call was still processing. The call has been canceled.";
                    break;
                case WSAEINVALIDPROCTABLE:
                    strRet = "The procedure call table is invalid.";
                    break;
                case WSAEINVALIDPROVIDER:
                    strRet = "The requested service provider is invalid.";
                    break;
                case WSAEPROVIDERFAILEDINIT:
                    strRet = "The requested service provider could not be loaded or initialized.";
                    break;
                case WSASYSCALLFAILURE:
                    strRet = "A system call that should never fail has failed.";
                    break;
                case WSASERVICE_NOT_FOUND:
                    strRet = "No such service is known. The service cannot be found in the specified name space.";
                    break;
                case WSATYPE_NOT_FOUND:
                    strRet = "The specified class was not found.";
                    break;
                case WSA_E_NO_MORE:
                    strRet = "No more results can be returned by WSALookupServiceNext.";
                    break;
                case WSA_E_CANCELLED:
                    strRet = "A call to WSALookupServiceEnd was made while this call was still processing. The call has been canceled.";
                    break;
                case WSAEREFUSED:
                    strRet = "A database query failed because it was actively refused.";
                    break;
                case WSAHOST_NOT_FOUND:
                    strRet = "No such host is known.";
                    break;
                case WSATRY_AGAIN:
                    strRet = "This is usually a temporary error during hostname resolution and means that the local server did not receive a response from an authoritative server.";
                    break;
                case WSANO_RECOVERY:
                    strRet = "A non-recoverable error occurred during a database lookup.";
                    break;
                case WSANO_DATA:
                    strRet = "The requested name is valid, but no data of the requested type was found.";
                    break;
                case WSA_QOS_RECEIVERS:
                    strRet = "At least one reserve has arrived.";
                    break;
                case WSA_QOS_SENDERS:
                    strRet = "At least one path has arrived.";
                    break;
                case WSA_QOS_NO_SENDERS:
                    strRet = "There are no senders.";
                    break;
                case WSA_QOS_NO_RECEIVERS:
                    strRet = "There are no receivers.";
                    break;
                case WSA_QOS_REQUEST_CONFIRMED:
                    strRet = "Reserve has been confirmed.";
                    break;
                case WSA_QOS_ADMISSION_FAILURE:
                    strRet = "Error due to lack of resources.";
                    break;
                case WSA_QOS_POLICY_FAILURE:
                    strRet = "Rejected for administrative reasons - bad credentials.";
                    break;
                case WSA_QOS_BAD_STYLE:
                    strRet = "Unknown or conflicting style.";
                    break;
                case WSA_QOS_BAD_OBJECT:
                    strRet = "Problem with some part of the filterspec or providerspecific buffer in general.";
                    break;
                case WSA_QOS_TRAFFIC_CTRL_ERROR:
                    strRet = "Problem with some part of the flowspec.";
                    break;
                case WSA_QOS_GENERIC_ERROR:
                    strRet = "General QOS error.";
                    break;
                case WSA_QOS_ESERVICETYPE:
                    strRet = "An invalid or unrecognized service type was found in the flowspec.";
                    break;
                case WSA_QOS_EFLOWSPEC:
                    strRet = "An invalid or inconsistent flowspec was found in the QOS structure.";
                    break;
                case WSA_QOS_EPROVSPECBUF:
                    strRet = "Invalid QOS provider-specific buffer.";
                    break;
                case WSA_QOS_EFILTERSTYLE:
                    strRet = "An invalid QOS filter style was used.";
                    break;
                case WSA_QOS_EFILTERTYPE:
                    strRet = "An invalid QOS filter type was used.";
                    break;
                case WSA_QOS_EFILTERCOUNT:
                    strRet = "An incorrect number of QOS FILTERSPECs were specified in the FLOWDESCRIPTOR.";
                    break;
                case WSA_QOS_EOBJLENGTH:
                    strRet = "An object with an invalid ObjectLength field was specified in the QOS provider-specific buffer.";
                    break;
                case WSA_QOS_EFLOWCOUNT:
                    strRet = "An incorrect number of flow descriptors was specified in the QOS structure.";
                    break;
                case WSA_QOS_EUNKNOWNPSOBJ:
                    strRet = "An unrecognized object was found in the QOS provider-specific buffer.";
                    break;
                case WSA_QOS_EPOLICYOBJ:
                    strRet = "An invalid policy object was found in the QOS provider-specific buffer.";
                    break;
                case WSA_QOS_EFLOWDESC:
                    strRet = "An invalid QOS flow descriptor was found in the flow descriptor list.";
                    break;
                case WSA_QOS_EPSFLOWSPEC:
                    strRet = "An invalid or inconsistent flowspec was found in the QOS provider-specific buffer.";
                    break;
                case WSA_QOS_EPSFILTERSPEC:
                    strRet = "An invalid FILTERSPEC was found in the QOS provider-specific buffer.";
                    break;
                case WSA_QOS_ESDMODEOBJ:
                    strRet = "An invalid shape discard mode object was found in the QOS provider-specific buffer.";
                    break;
                case WSA_QOS_ESHAPERATEOBJ:
                    strRet = "An invalid shaping rate object was found in the QOS provider-specific buffer.";
                    break;
                case WSA_QOS_RESERVED_PETYPE:
                    strRet = "A reserved policy element was found in the QOS provider-specific buffer.";
                    break;
                default:
                    strRet = "";
                    break;
            }

            return strRet;

        }

        #endregion

        #endregion     
    }
}
