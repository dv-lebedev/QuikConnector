﻿using System;
using System.Runtime.InteropServices;

namespace QuikConnector
{
    /// <summary>
    /// TransToQuik.dll
    /// </summary>
    internal static class QuikApi
    {
        public const long TRANS2QUIK_SUCCESS = 0;
        public const long TRANS2QUIK_FAILED = 1;
        public const long TRANS2QUIK_QUIK_TERMINAL_NOT_FOUND = 2;
        public const long TRANS2QUIK_DLL_VERSION_NOT_SUPPORTED = 3;
        public const long TRANS2QUIK_ALREADY_CONNECTED_TO_QUIK = 4;
        public const long TRANS2QUIK_WRONG_SYNTAX = 5;
        public const long TRANS2QUIK_QUIK_NOT_CONNECTED = 6;
        public const long TRANS2QUIK_DLL_NOT_CONNECTED = 7;
        public const long TRANS2QUIK_QUIK_CONNECTED = 8;
        public const long TRANS2QUIK_QUIK_DISCONNECTED = 9;
        public const long TRANS2QUIK_DLL_CONNECTED = 10;
        public const long TRANS2QUIK_DLL_DISCONNECTED = 11;
        public const long TRANS2QUIK_MEMORY_ALLOCATION_ERROR = 12;
        public const long TRANS2QUIK_WRONG_CONNECTION_HANDLE = 13;
        public const long TRANS2QUIK_WRONG_INPUT_PARAMS = 14;

        public static string ResultToString(long Result)
        {
            switch (Result)
            {
                case TRANS2QUIK_SUCCESS:                                //0
                    return "TRANS2QUIK_SUCCESS";
                case TRANS2QUIK_FAILED:                                 //1
                    return "TRANS2QUIK_FAILED";
                case TRANS2QUIK_QUIK_TERMINAL_NOT_FOUND:                //2
                    return "TRANS2QUIK_QUIK_TERMINAL_NOT_FOUND";
                case TRANS2QUIK_DLL_VERSION_NOT_SUPPORTED:              //3
                    return "TRANS2QUIK_DLL_VERSION_NOT_SUPPORTED";
                case TRANS2QUIK_ALREADY_CONNECTED_TO_QUIK:              //4
                    return "TRANS2QUIK_ALREADY_CONNECTED_TO_QUIK";
                case TRANS2QUIK_WRONG_SYNTAX:                           //5
                    return "TRANS2QUIK_WRONG_SYNTAX";
                case TRANS2QUIK_QUIK_NOT_CONNECTED:                     //6
                    return "TRANS2QUIK_QUIK_NOT_CONNECTED";
                case TRANS2QUIK_DLL_NOT_CONNECTED:                      //7
                    return "TRANS2QUIK_DLL_NOT_CONNECTED";
                case TRANS2QUIK_QUIK_CONNECTED:                         //8
                    return "TRANS2QUIK_QUIK_CONNECTED";
                case TRANS2QUIK_QUIK_DISCONNECTED:                      //9
                    return "TRANS2QUIK_QUIK_DISCONNECTED";
                case TRANS2QUIK_DLL_CONNECTED:                          //10
                    return "TRANS2QUIK_DLL_CONNECTED";
                case TRANS2QUIK_DLL_DISCONNECTED:                       //11
                    return "TRANS2QUIK_DLL_DISCONNECTED";
                case TRANS2QUIK_MEMORY_ALLOCATION_ERROR:                //12
                    return "TRANS2QUIK_MEMORY_ALLOCATION_ERROR";
                case TRANS2QUIK_WRONG_CONNECTION_HANDLE:                //13
                    return "TRANS2QUIK_WRONG_CONNECTION_HANDLE";
                case TRANS2QUIK_WRONG_INPUT_PARAMS:                     //14
                    return "TRANS2QUIK_WRONG_INPUT_PARAMS";
                default:
                    return "UNKNOWN_VALUE";
            }
        }
        public static string ByteToString(byte[] Str)
        {
            int count = 0;
            for (int i = 0; i < Str.Length; ++i)
            {
                if (0 == Str[i])
                {
                    count = i;
                    break;
                }
            }
            return System.Text.Encoding.Default.GetString(Str, 0, count);
        }

        #region connect
        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_CONNECT@16", CallingConvention = CallingConvention.Winapi)]
        public static extern long connect(string lpcstrConnectionParamsString, ref long pnExtendedErrorCode,
           byte[] lpstrErrorMessage, UInt32 dwErrorMessageSize);

        public static long Connect(string path)
        {
            Byte[] EMsg = new Byte[50];
            UInt32 EMsgSz = 50;
            long ExtEC = 0;

            return (connect(path, ref ExtEC, EMsg, EMsgSz) & 255);
        }
        #endregion

        #region is_dll_connected
        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_IS_DLL_CONNECTED@12",
            CallingConvention = CallingConvention.StdCall)]
        static extern long is_dll_connected(
            ref long pnExtendedErrorCode,
            byte[] lpstrErrorMessage,
            UInt32 dwErrorMessageSize);
        public static void is_dll_connected_test(bool FinalPause)
        {
            Byte[] EMsg = new Byte[50];
            UInt32 EMsgSz = 50;
            long ExtEC = 0, rez = -1;
            rez = is_dll_connected(ref ExtEC, EMsg, EMsgSz);
            //Console.WriteLine("test_q.is_dll_connected_test>\t{0} {1}", (rez & 255), ResultToString(rez & 255));
            if (FinalPause) Console.ReadLine();
        }
        public static bool IsDLLConnected()
        {
            Byte[] EMsg = new Byte[50];
            UInt32 EMsgSz = 50;
            long ExtEC = 0, rez = -1;
            rez = is_dll_connected(ref ExtEC, EMsg, EMsgSz) & 255;
            if (rez == 10)
                return true;
            else
                return false;
        }
        #endregion

        #region disconnect
        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_DISCONNECT@12",
          CallingConvention = CallingConvention.StdCall)]
        static extern long disconnect(
            ref long pnExtendedErrorCode,
            byte[] lpstrErrorMessage,
            UInt32 dwErrorMessageSize);

        public static long Disconnect()
        {
            Byte[] dEMsg = new Byte[50];
            UInt32 dEMsgSz = 50;
            long dExtEC = 0;
            return (disconnect(ref dExtEC, dEMsg, dEMsgSz) & 255);
        }

        #endregion

        #region is_quik_connected
        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_IS_QUIK_CONNECTED@12",
            CallingConvention = CallingConvention.StdCall)]
        static extern long is_quik_connected(
             ref long pnExtendedErrorCode,
             byte[] lpstrErrorMessage,
             UInt32 dwErrorMessageSize);
        public static void IsQuikConnected(ref bool ret)
        {
            Byte[] EMsg = new Byte[50];
            long ExtEC = 0, rez = -1;
            UInt32 EMsgSz = 50;
            rez = is_quik_connected(ref ExtEC, EMsg, EMsgSz) & 255;
            if (rez == 8)
                ret = true;
            else
                ret = false;
        }
        public static bool IsQuikConnected()
        {
            Byte[] EMsg = new Byte[50];
            long ExtEC = 0, rez = -1;
            UInt32 EMsgSz = 50;
            rez = is_quik_connected(ref ExtEC, EMsg, EMsgSz) & 255;
            if (rez == 8)
                return true;
            else
                return false;
        }
        public static void is_quik_connected_test(bool FinalPause)
        {
            Byte[] EMsg = new Byte[50];
            long ExtEC = 0, rez = -1;
            UInt32 EMsgSz = 50;
            rez = is_quik_connected(ref ExtEC, EMsg, EMsgSz);
            //Console.WriteLine("test_q.is_quik_connected_test>\t{0} {1}", (rez & 255), ResultToString(rez & 255));
            //Console.WriteLine(" ExtEC={0}, EMsg={1}, EMsgSz={2}",(ExtEC & 255).ToString(), ByteToString(EMsg), EMsgSz);
            if (FinalPause) Console.ReadLine();
        }
        #endregion

        #region send_sync_transaction
        //long TRANS2QUIK_API __stdcall TRANS2QUIK_SEND_SYNC_TRANSACTION (
        //    LPSTR lpstTransactionString, 
        //    long* pnReplyCode, 
        //    PDWORD pdwTransId, 
        //    double* pdOrderNum, 
        //    LPSTR lpstrResultMessage, 
        //    DWORD dwResultMessageSize, 
        //    long* pnExtendedErrorCode, 
        //    LPSTR lpstErrorMessage, 
        //    DWORD dwErrorMessageSize);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_SEND_SYNC_TRANSACTION@36",
            CallingConvention = CallingConvention.StdCall)]
        static extern long send_sync_transaction(
            string lpstTransactionString,
            ref long pnReplyCode,
            ref int pdwTransId,
            ref double pdOrderNum,
            byte[] lpstrResultMessage,
            UInt32 dwResultMessageSize,
            ref long pnExtendedErrorCode,
            byte[] lpstrErrorMessage,
            UInt32 dwErrorMessageSize);

        public static void send_sync_transaction_test(string transactionStr, ref double OrderNum)
        {
            //Console.Write("\ntest_q.send_sync_transaction_test>\n");
            Byte[] EMsg = new Byte[50];
            Byte[] ResMsg = new Byte[50];
            long ExtEC = -100, rez = -1, ReplyCd = 0;
            int TransID = 0;
            UInt32 ResMsgSz = 50, EMsgSz = 50;
            rez = send_sync_transaction(transactionStr, ref ReplyCd, ref TransID, ref OrderNum,
                 ResMsg, ResMsgSz, ref ExtEC, EMsg, EMsgSz);
            Console.WriteLine("{0} {1}", (rez & 255), ResultToString(rez & 255));
            Console.WriteLine(" ExtEC={0}, EMsg={1}, EMsgSz={2}",
                (ExtEC & 255).ToString(), ByteToString(EMsg), EMsgSz);
            String resStr = ByteToString(ResMsg);
            resStr = resStr.Trim();
            Console.WriteLine(" ReplyCode={0} TransID={1}  OrderNum={2} \n ResMsg={3}, ResMsgSz={4}",
                ReplyCd, TransID, OrderNum, resStr, ResMsgSz);
        }
        #endregion

        #region send_async_transaction
        public delegate void transaction_reply_callback(
            Int32 nTransactionResult,
            Int32 nTransactionExtendedErrorCode,
            Int32 nTransactionReplyCode,
            UInt32 dwTransId,
            Double dOrderNum,
            [MarshalAs(UnmanagedType.LPStr)] string TransactionReplyMessage);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_SET_TRANSACTIONS_REPLY_CALLBACK@16", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 set_transaction_reply_callback(
            transaction_reply_callback pfTransactionReplyCallback,
            ref Int32 pnExtendedErrorCode,
            byte[] lpstrErrorMessage,
            UInt32 dwErrorMessageSize);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_SEND_ASYNC_TRANSACTION@16", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 send_async_transaction(
            [MarshalAs(UnmanagedType.LPStr)]string transactionString,
            ref Int32 nExtendedErrorCode,
            byte[] lpstrErrorMessage,
            UInt32 dwErrorMessageSize);

        public static Int32 send_async_transaction_test(string transactionString)
        {
            UInt32 err_msg_size = 256;
            Byte[] err_msg = new Byte[err_msg_size];
            Int32 nExtendedErrorCode = 0;

            Int32 res = send_async_transaction(transactionString, ref nExtendedErrorCode, err_msg, err_msg_size);

            return res;
        }

        public static void transaction_reply_callback_impl(
            Int32 nTransactionResult,
            Int32 nTransactionExtendedErrorCode,
            Int32 nTransactionReplyCode,
            UInt32 dwTransId,
            Double dOrderNum,
            [MarshalAs(UnmanagedType.LPStr)] string TransactionReplyMessage)
        {
            String strOutput = "TrRes=" + nTransactionResult +
                " TrResStr=" + ResultToString(nTransactionResult & 255) +
                " TrExErrCode=" + nTransactionExtendedErrorCode +
                " TrReplyCode=" + nTransactionReplyCode +
                " TrID=" + dwTransId +
                " OrderNum=" + dOrderNum +
                " ResMsg=" + TransactionReplyMessage;

            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"async_trans.log", true, System.Text.Encoding.GetEncoding(1251)))
                {
                    file.WriteLine(strOutput);
                    Console.WriteLine("TRANS_REPLY_CALLBACK: " + strOutput);
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion

        #region orders
        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_SUBSCRIBE_ORDERS@8",
            CallingConvention = CallingConvention.StdCall)]
        public static extern long subscribe_orders([MarshalAs(UnmanagedType.LPStr)]string class_code, [MarshalAs(UnmanagedType.LPStr)]string sec_code);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_UNSUBSCRIBE_ORDERS@0",
            CallingConvention = CallingConvention.StdCall)]
        public static extern long ubsubscribe_orders();

        public delegate void order_status_callback(
                Int32 nMode,
                UInt32 dwTransID,
                Double dNumber,
                [MarshalAs(UnmanagedType.LPStr)]string ClassCode,
                [MarshalAs(UnmanagedType.LPStr)]string SecCode,
                Double dPrice,
                Int32 nBalance,
                Double dValue,
                Int32 nIsSell,
                Int32 nStatus,
                Int32 nOrderDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_START_ORDERS@4", CallingConvention = CallingConvention.StdCall)]
        public static extern long start_orders(
            order_status_callback pfOrderStatusCallback);

        #region order_descriptor_functions
        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_ORDER_QTY@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 TRANS2QUIK_ORDER_QTY(Int32 nOrderDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_ORDER_DATE@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 TRANS2QUIK_ORDER_DATE(Int32 nOrderDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_ORDER_TIME@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 TRANS2QUIK_ORDER_TIME(Int32 nOrderDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_ORDER_ACTIVATION_TIME@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 TRANS2QUIK_ORDER_ACTIVATION_TIME(Int32 nOrderDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_ORDER_WITHDRAW_TIME@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 TRANS2QUIK_ORDER_WITHDRAW_TIME(Int32 nOrderDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_ORDER_EXPIRY@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 TRANS2QUIK_ORDER_EXPIRY(Int32 nOrderDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_ORDER_ACCRUED_INT@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern double TRANS2QUIK_ORDER_ACCRUED_INT(Int32 nOrderDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_ORDER_YIELD@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern double TRANS2QUIK_ORDER_YIELD(Int32 nOrderDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_ORDER_UID@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 TRANS2QUIK_ORDER_UID(Int32 nOrderDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_ORDER_USERID@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr TRANS2QUIK_ORDER_USERID_IMPL(Int32 nOrderDescriptor);

        public static string TRANS2QUIK_ORDER_USERID(Int32 nOrderDescriptor)
        {
            return Marshal.PtrToStringAnsi(TRANS2QUIK_ORDER_USERID_IMPL(nOrderDescriptor));
        }

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_ORDER_ACCOUNT@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr TRANS2QUIK_ORDER_ACCOUNT_IMPL(Int32 nOrderDescriptor);

        public static string TRANS2QUIK_ORDER_ACCOUNT(Int32 nOrderDescriptor)
        {
            return Marshal.PtrToStringAnsi(TRANS2QUIK_ORDER_ACCOUNT_IMPL(nOrderDescriptor));
        }

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_ORDER_BROKERREF@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr TRANS2QUIK_ORDER_BROKERREF_IMPL(Int32 nOrderDescriptor);

        public static string TRANS2QUIK_ORDER_BROKERREF(Int32 nOrderDescriptor)
        {
            return Marshal.PtrToStringAnsi(TRANS2QUIK_ORDER_BROKERREF_IMPL(nOrderDescriptor));
        }

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_ORDER_CLIENT_CODE@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr TRANS2QUIK_ORDER_CLIENT_CODE_IMPL(Int32 nOrderDescriptor);

        public static string TRANS2QUIK_ORDER_CLIENT_CODE(Int32 nOrderDescriptor)
        {
            return Marshal.PtrToStringAnsi(TRANS2QUIK_ORDER_CLIENT_CODE_IMPL(nOrderDescriptor));
        }

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_ORDER_FIRMID@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr TRANS2QUIK_ORDER_FIRMID_IMPL(Int32 nOrderDescriptor);

        public static string TRANS2QUIK_ORDER_FIRMID(Int32 nOrderDescriptor)
        {
            return Marshal.PtrToStringAnsi(TRANS2QUIK_ORDER_FIRMID_IMPL(nOrderDescriptor));
        }


        #endregion

        public static void order_status_callback_impl(
                Int32 nMode, UInt32 dwTransID, Double dNumber, string ClassCode, string SecCode,
                Double dPrice, Int32 nBalance, Double dValue, Int32 nIsSell, Int32 nStatus, Int32 nOrderDescriptor)
        {
            OrderCallbackEventArgs ocp = new OrderCallbackEventArgs
            {
                Mode = nMode,
                TransID = dwTransID,
                Number = dNumber,
                ClassCode = ClassCode,
                SecCode = SecCode,
                Price = dPrice,
                Balance = nBalance,
                Value = dValue,
                IsSell = nIsSell,
                Status = nStatus,
                OrderDescriptor = nOrderDescriptor
            };

            if (OrderCallback != null) OrderCallback(null, ocp);
        }
        #endregion

        #region trades
        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_SUBSCRIBE_TRADES@8",
            CallingConvention = CallingConvention.StdCall)]
        public static extern long subscribe_trades([MarshalAs(UnmanagedType.LPStr)]string class_code, [MarshalAs(UnmanagedType.LPStr)]string sec_code);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_UNSUBSCRIBE_TRADES@0",
            CallingConvention = CallingConvention.StdCall)]
        public static extern long ubsubscribe_trades();

        public delegate void trade_status_callback(
                Int32 nMode,
                Double dNumber,
                Double dOrderNumber,
                [MarshalAs(UnmanagedType.LPStr)]string ClassCode,
                [MarshalAs(UnmanagedType.LPStr)]string SecCode,
                Double dPrice,
                Int32 nQty,
                Double dValue,
                Int32 nIsSell,
                Int32 nOrderDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_START_TRADES@4", CallingConvention = CallingConvention.StdCall)]
        public static extern long start_trades(
            trade_status_callback pfTradeStatusCallback);

        #region trade_descriptor_functions
        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_DATE@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 TRANS2QUIK_TRADE_DATE(Int32 nTradeDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_SETTLE_DATE@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 TRANS2QUIK_TRADE_SETTLE_DATE(Int32 nTradeDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_TIME@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 TRANS2QUIK_TRADE_TIME(Int32 nTradeDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_IS_MARGINAL@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 TRANS2QUIK_TRADE_IS_MARGINAL(Int32 nTradeDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_ACCRUED_INT@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern double TRANS2QUIK_TRADE_ACCRUED_INT(Int32 nTradeDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_YIELD@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern double TRANS2QUIK_TRADE_YIELD(Int32 nTradeDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_TS_COMMISSION@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern double TRANS2QUIK_TRADE_TS_COMMISSION(Int32 nTradeDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_CLEARING_CENTER_COMMISSION@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern double TRANS2QUIK_TRADE_CLEARING_CENTER_COMMISSION(Int32 nTradeDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_EXCHANGE_COMMISSION@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern double TRANS2QUIK_TRADE_EXCHANGE_COMMISSION(Int32 nTradeDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_TRADING_SYSTEM_COMMISSION@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern double TRANS2QUIK_TRADE_TRADING_SYSTEM_COMMISSION(Int32 nTradeDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_PRICE2@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern double TRANS2QUIK_TRADE_PRICE2(Int32 nTradeDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_REPO_RATE@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern double TRANS2QUIK_TRADE_REPO_RATE(Int32 nTradeDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_REPO_VALUE@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern double TRANS2QUIK_TRADE_REPO_VALUE(Int32 nTradeDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_REPO2_VALUE@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern double TRANS2QUIK_TRADE_REPO2_VALUE(Int32 nTradeDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_ACCRUED_INT2@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern double TRANS2QUIK_TRADE_ACCRUED_INT2(Int32 nTradeDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_REPO_TERM@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 TRANS2QUIK_TRADE_REPO_TERM(Int32 nTradeDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_START_DISCOUNT@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern double TRANS2QUIK_TRADE_START_DISCOUNT(Int32 nTradeDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_LOWER_DISCOUNT@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern double TRANS2QUIK_TRADE_LOWER_DISCOUNT(Int32 nTradeDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_UPPER_DISCOUNT@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern double TRANS2QUIK_TRADE_UPPER_DISCOUNT(Int32 nTradeDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_BLOCK_SECURITIES@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 TRANS2QUIK_TRADE_BLOCK_SECURITIES(Int32 nTradeDescriptor);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_CURRENCY@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr TRANS2QUIK_TRADE_CURRENCY_IMPL(Int32 nTradeDescriptor);

        public static string TRANS2QUIK_TRADE_CURRENCY(Int32 nTradeDescriptor)
        {
            return Marshal.PtrToStringAnsi(TRANS2QUIK_TRADE_CURRENCY_IMPL(nTradeDescriptor));
        }

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_SETTLE_CURRENCY@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr TRANS2QUIK_TRADE_SETTLE_CURRENCY_IMPL(Int32 nTradeDescriptor);

        public static string TRANS2QUIK_TRADE_SETTLE_CURRENCY(Int32 nTradeDescriptor)
        {
            return Marshal.PtrToStringAnsi(TRANS2QUIK_TRADE_SETTLE_CURRENCY_IMPL(nTradeDescriptor));
        }

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_SETTLE_CODE@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr TRANS2QUIK_TRADE_SETTLE_CODE_IMPL(Int32 nTradeDescriptor);

        public static string TRANS2QUIK_TRADE_SETTLE_CODE(Int32 nTradeDescriptor)
        {
            return Marshal.PtrToStringAnsi(TRANS2QUIK_TRADE_SETTLE_CODE_IMPL(nTradeDescriptor));
        }

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_ACCOUNT@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr TRANS2QUIK_TRADE_ACCOUNT_IMPL(Int32 nTradeDescriptor);

        public static string TRANS2QUIK_TRADE_ACCOUNT(Int32 nTradeDescriptor)
        {
            return Marshal.PtrToStringAnsi(TRANS2QUIK_TRADE_ACCOUNT_IMPL(nTradeDescriptor));
        }

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_BROKERREF@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr TRANS2QUIK_TRADE_BROKERREF_IMPL(Int32 nTradeDescriptor);

        public static string TRANS2QUIK_TRADE_BROKERREF(Int32 nTradeDescriptor)
        {
            return Marshal.PtrToStringAnsi(TRANS2QUIK_TRADE_BROKERREF_IMPL(nTradeDescriptor));
        }

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_CLIENT_CODE@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr TRANS2QUIK_TRADE_CLIENT_CODE_IMPL(Int32 nTradeDescriptor);

        public static string TRANS2QUIK_TRADE_CLIENT_CODE(Int32 nTradeDescriptor)
        {
            return Marshal.PtrToStringAnsi(TRANS2QUIK_TRADE_CLIENT_CODE_IMPL(nTradeDescriptor));
        }

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_USERID@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr TRANS2QUIK_TRADE_USERID_IMPL(Int32 nTradeDescriptor);

        public static string TRANS2QUIK_TRADE_USERID(Int32 nTradeDescriptor)
        {
            return Marshal.PtrToStringAnsi(TRANS2QUIK_TRADE_USERID_IMPL(nTradeDescriptor));
        }

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_FIRMID@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr TRANS2QUIK_TRADE_FIRMID_IMPL(Int32 nTradeDescriptor);

        public static string TRANS2QUIK_TRADE_FIRMID(Int32 nTradeDescriptor)
        {
            return Marshal.PtrToStringAnsi(TRANS2QUIK_TRADE_FIRMID_IMPL(nTradeDescriptor));
        }

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_PARTNER_FIRMID@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr TRANS2QUIK_TRADE_PARTNER_FIRMID_IMPL(Int32 nTradeDescriptor);

        public static string TRANS2QUIK_TRADE_PARTNER_FIRMID(Int32 nTradeDescriptor)
        {
            return Marshal.PtrToStringAnsi(TRANS2QUIK_TRADE_PARTNER_FIRMID_IMPL(nTradeDescriptor));
        }

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_EXCHANGE_CODE@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr TRANS2QUIK_TRADE_EXCHANGE_CODE_IMPL(Int32 nTradeDescriptor);

        public static string TRANS2QUIK_TRADE_EXCHANGE_CODE(Int32 nTradeDescriptor)
        {
            return Marshal.PtrToStringAnsi(TRANS2QUIK_TRADE_EXCHANGE_CODE_IMPL(nTradeDescriptor));
        }

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_TRADE_STATION_ID@4",
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr TRANS2QUIK_TRADE_STATION_ID_IMPL(Int32 nTradeDescriptor);

        public static string TRANS2QUIK_TRADE_STATION_ID(Int32 nTradeDescriptor)
        {
            return Marshal.PtrToStringAnsi(TRANS2QUIK_TRADE_STATION_ID_IMPL(nTradeDescriptor));
        }

        #endregion

        public static void trade_status_callback_implementation(
                Int32 nMode, Double dNumber, Double dOrderNumber, string ClassCode, string SecCode,
                Double dPrice, Int32 nQty, Double dValue, Int32 nIsSell, Int32 nTradeDescriptor)
        {
            TradeCallbackEventArgs tcp = new TradeCallbackEventArgs
            {
                Mode = nMode,
                Number = dNumber,
                OrderNumber = dOrderNumber,
                ClassCode = ClassCode,
                SecCode = SecCode,
                Price = dPrice,
                Qty = nQty,
                Value = dValue,
                IsSell = nIsSell,
                TradeDescription = nTradeDescriptor
            };

            if (TradeCallBack != null) TradeCallBack(null, tcp);
        }


        #endregion

        #region Делегаты
        #region connection_status_callback

        public delegate void connection_status_callback(UInt32 nConnectionEvent, UInt32 nExtendedErrorCode, byte[] lpstrInfoMessage);

        [DllImport("TRANS2QUIK.DLL", EntryPoint = "_TRANS2QUIK_SET_CONNECTION_STATUS_CALLBACK@16", CallingConvention = CallingConvention.StdCall)]
        public static extern long set_connection_status_callback(
            connection_status_callback pfConnectionStatusCallback,
            UInt32 pnExtendedErrorCode,
            byte[] lpstrErrorMessage,
            UInt32 dwErrorMessageSize);

        public static void connection_status_callback_Report(
            UInt32 nConnectionEvent, UInt32 nExtendedErrorCode, byte[] lpstrInfoMessage)
        {
            if (ConnectionStatusCallback != null)
                ConnectionStatusCallback(null, new ConnectionStatusEventArgs
                {
                    nConnectionEvent = nConnectionEvent,
                    lpstrInfoMessage = lpstrInfoMessage,
                    nExtendedErrorCode = nExtendedErrorCode,
                });
        }
        #endregion

        #endregion

        #region subscribes

        public static event EventHandler<OrderCallbackEventArgs> OrderCallback;
        public static event EventHandler<TradeCallbackEventArgs> TradeCallBack;
        public static event EventHandler<ConnectionStatusEventArgs> ConnectionStatusCallback;


        public static void MultiSubscribe()
        {

            order_status_callback order_callback = new order_status_callback(order_status_callback_impl);
            trade_status_callback trade_callback = new trade_status_callback(trade_status_callback_implementation);
            connection_status_callback conn_cb = new connection_status_callback(connection_status_callback_Report);
            transaction_reply_callback trans_callback = new transaction_reply_callback(transaction_reply_callback_impl);

            GCHandle gcOrder = GCHandle.Alloc(order_callback);
            GCHandle gcTrade = GCHandle.Alloc(trade_callback);
            GCHandle gcConn = GCHandle.Alloc(conn_cb);
            GCHandle gcTrans = GCHandle.Alloc(trans_callback);

            Byte[] EMsg = new Byte[50];
            UInt32 EMsgSz = 50, uExtEC = 0;
            Int32 ExtEC = 0;

            set_connection_status_callback(conn_cb, uExtEC, EMsg, EMsgSz);
            set_transaction_reply_callback(trans_callback, ref ExtEC, EMsg, EMsgSz);

            subscribe_trades("", "");
            start_trades(trade_callback);

            subscribe_orders("", "");
            start_orders(order_callback);
        }

        #endregion

    }
}
