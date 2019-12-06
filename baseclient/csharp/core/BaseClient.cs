// This file is auto-generated, don't edit it. Thanks.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

using AlibabaCloud.OSS.Attributes;
using AlibabaCloud.OSS.Models;
using AlibabaCloud.OSS.Utils;

using Aliyun.Credentials;
using Aliyun.Credentials.Utils;

using Newtonsoft.Json;

using Tea;

[assembly : InternalsVisibleTo("UnitTests")]

namespace AlibabaCloud.OSS
{
    public class BaseClient
    {
        protected string _regionId;
        protected string _endpoint;
        protected string _protocol;
        protected readonly string _defaultUserAgent;
        protected string _userAgent;
        protected string _hostModel;
        protected bool _isEnableMD5;
        protected bool _isEnableCrc;
        protected int _readTimeout;
        protected int _connectTimeout;
        protected string _localAddr;
        protected string _httpProxy;
        protected string _httpsProxy;
        protected string _noProxy;
        protected string _socks5Proxy;
        protected string _socks5NetWork;
        protected int _maxIdleConns;
        protected object _logger;
        protected Dictionary<string, object> _config;

        internal Credential credential;

        private string _signatureVersion;
        private string[] _addtionalHeaders;

        private char[] characters = { '-', '_', '.', '~' };

        public string SignatureVersion
        {
            get
            {
                return _signatureVersion;
            }
            set
            {
                _signatureVersion = value;
            }
        }

        public string[] AddtionalHeaders
        {
            get
            {
                return _addtionalHeaders;
            }
            set
            {
                _addtionalHeaders = value;
            }
        }

        public BaseClient(Dictionary<string, object> config)
        {
            _regionId = DictUtils.GetDicValue(config, "regionId").ToSafeString();
            _protocol = DictUtils.GetDicValue(config, "protocol").ToSafeString();
            _endpoint = DictUtils.GetDicValue(config, "endpoint").ToSafeString();
            _config = config;

            _defaultUserAgent = GetDefaultUserAgent();

            SetCredential(config);
        }

        protected void SetCredential(Dictionary<string, object> config)
        {
            Configuration credentialConfig = new Configuration()
            {
                AccessKeyId = DictUtils.GetDicValue(config, "accessKeyId").ToSafeString(),
                AccessKeySecret = DictUtils.GetDicValue(config, "accessKeySecret").ToSafeString(),
                SecurityToken = DictUtils.GetDicValue(config, "securityToken").ToSafeString(),
                Type = DictUtils.GetDicValue(config, "type").ToSafeString()
            };
            if (string.IsNullOrWhiteSpace(credentialConfig.Type))
            {
                credentialConfig.Type = AuthConstant.AccessKey;
            }
            credential = new Credential(credentialConfig);
        }

        protected string _getDate()
        {
            return DateTime.UtcNow.ToUniversalTime().GetDateTimeFormats('r') [0];
        }

        protected string _getAuth(TeaRequest teaRequest, string bucketName)
        {
            string auth = string.Empty;
            if (credential == null)
            {
                return auth;
            }
            try
            {
                string accessKeyId = credential.AccessKeyId;
                string accessKeySecret = credential.AccessKeySecret;
                string securityToken = credential.SecurityToken;
                if (!string.IsNullOrWhiteSpace(securityToken))
                {
                    if (teaRequest.Headers == null)
                    {
                        teaRequest.Headers = new Dictionary<string, string>();
                    }
                    teaRequest.Headers.Add("X-Oss-Security-Token", securityToken);
                }

                if (_signatureVersion.ToSafeString("").ToLower() == "v2")
                {
                    if (_addtionalHeaders == null)
                    {
                        _addtionalHeaders = new string[0];
                    }
                    auth = GetSignatureV2(teaRequest, bucketName, accessKeyId, accessKeySecret, _addtionalHeaders);
                }
                else
                {
                    auth = GetSignatureV1(teaRequest, bucketName, accessKeyId, accessKeySecret);
                }
            }
            catch
            {
                auth = string.Empty;
            }

            return auth;
        }

        protected Dictionary<string, object> _parseXml(TeaResponse response, Type type)
        {
            string content = TeaCore.GetResponseBody(response);
            return XmlUtil.DeserializeXml(content, type);
        }

        protected string _getHost(string bucketName)
        {
            string host = string.Empty;
            if (string.IsNullOrWhiteSpace(_regionId))
            {
                _regionId = "cn-hangzhou";
            }
            if (string.IsNullOrWhiteSpace(_endpoint))
            {
                _endpoint = "oss-" + _regionId + ".aliyuncs.com";
            }
            if (!string.IsNullOrWhiteSpace(bucketName))
            {
                _hostModel = _hostModel == null?string.Empty : _hostModel;
                if (_hostModel.ToLower() == "ip")
                {
                    host = _endpoint + "/" + bucketName;
                }
                else if (_hostModel.ToLower() == "cname")
                {
                    host = _endpoint;
                }
                else
                {
                    host = bucketName + "." + _endpoint;
                }
            }
            else
            {
                host = _endpoint;
            }
            return host;
        }

        public string _default(string strValue, string strDefault)
        {
            if (string.IsNullOrWhiteSpace(strValue))
            {
                return strDefault;
            }
            return strValue;
        }

        public int? _defaultNumber(int? numValue, int? numDefault)
        {
            if (numValue > 0)
            {
                return numValue;
            }
            return numDefault;
        }

        protected string _toBody(object obj)
        {
            return XmlUtil.SerializeXml(obj);
        }

        protected bool _isFail(TeaResponse teaResponse)
        {
            if (teaResponse.StatusCode >= 200 && teaResponse.StatusCode < 300)
            {
                return false;
            }

            return true;
        }

        protected Dictionary<string, string> _toQuery(Dictionary<string, object> dicRequest)
        {
            Dictionary<string, string> dicQuery = new Dictionary<string, string>();
            foreach (KeyValuePair<string, object> item in dicRequest)
            {
                if (item.Value != null)
                {
                    dicQuery.Add(item.Key, item.Value.ToString());
                }
            }
            return dicQuery;
        }

        protected Dictionary<string, string> _toMeta(Dictionary<string, string> meta, string prefix)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (meta == null)
            {
                return result;
            }
            foreach (var keypair in meta)
            {
                string newKey = keypair.Key;
                if (!keypair.Key.ToLower().StartsWith(prefix))
                {
                    newKey = prefix + keypair.Key;
                }
                result.Add(newKey, keypair.Value.ToSafeString());
            }
            return result;
        }

        protected Dictionary<string, string> _parseMeta(Dictionary<string, string> meta, string prefix)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (meta == null)
            {
                return result;
            }
            foreach (var keypair in meta)
            {
                string newKey = keypair.Key;
                if (keypair.Key.ToLower().StartsWith(prefix))
                {
                    newKey = keypair.Key.Replace(prefix, "");
                }
                result.Add(newKey, keypair.Value.ToSafeString());
            }
            return result;
        }

        protected string _getContentMD5(TeaRequest teaRequest, string md5Value, long md5Threshold)
        {
            if (!_isEnableMD5)
            {
                return string.Empty;
            }
            if (!string.IsNullOrWhiteSpace(md5Value))
            {
                return md5Value;
            }
            try
            {
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] data = md5.ComputeHash(teaRequest.Body);
                return Convert.ToBase64String(data);
            }
            catch
            {
                return string.Empty;
            }

        }

        protected string _getContentLength(TeaRequest teaRequest, string length)
        {
            if (!string.IsNullOrWhiteSpace(length))
            {
                return length;
            }
            return teaRequest.Body.Length.ToString();
        }

        protected string _getSpecialValue(object obj, string key)
        {
            Dictionary<string, string> ditc;
            try
            {
                string jsonStr = JsonConvert.SerializeObject(obj);
                ditc = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonStr);
                return DictUtils.GetDicValue(ditc, key);
            }
            catch
            {
                return string.Empty;
            }
        }

        protected string _getContentType(string filePath)
        {
            string type = Utils.MimeMapping.GetMimeMapping(filePath);
            if (string.IsNullOrWhiteSpace(type))
            {
                type = DictUtils.GetDicValue(StaticConst.extToMimeType, Path.GetExtension(filePath).ToLower());
            }
            return type;
        }

        protected bool _isNotCrcMatched(ulong? reqCrc, string respCrc)
        {
            if (_isEnableCrc)
            {
                if (!string.IsNullOrWhiteSpace(respCrc))
                {
                    return reqCrc == Convert.ToUInt64(respCrc);
                }
            }
            return false;
        }

        protected string _encode(string value, string encodeType)
        {
            string[] strs = value.Split('/');
            switch (encodeType)
            {
                case "Base64":
                    strs[strs.Length - 1] = Convert.ToBase64String(Encoding.UTF8.GetBytes(strs[strs.Length - 1]));
                    break;
                case "UrlEncode":
                    strs[strs.Length - 1] = HttpUtility.UrlEncode(strs[strs.Length - 1], Encoding.UTF8);
                    break;
            }
            value = string.Join("/", strs);
            return value;
        }

        protected string _base64Decode(string str)
        {
            string[] strs = str.Split('/');
            strs[strs.Length - 1] = Encoding.UTF8.GetString(Convert.FromBase64String(strs[strs.Length - 1]));
            return string.Join("/", strs);
        }

        protected string _urlDecode(string str)
        {
            string[] strs = str.Split('/');
            strs[strs.Length - 1] = HttpUtility.UrlDecode(strs[strs.Length - 1], Encoding.UTF8);
            return string.Join("/", strs);
        }

        protected ulong _parseUint(string respCrc, bool hasRange)
        {
            if (hasRange)
            {
                return 0;
            }
            try
            {
                return Convert.ToUInt64(respCrc);
            }
            catch
            {
                return 0;
            }
        }

        protected ulong? _getCrc(TeaRequest request, string contentlength, object listener, object tracker)
        {
            if (!_isEnableCrc)
            {
                return null;
            }
            try
            {
                ulong crc64 = 0;
                Crc64.InitECMA();
                byte[] buffer = new byte[4096];
                int bytesRead;
                while ((bytesRead = request.Body.Read(buffer, 0, buffer.Length)) != 0)
                {
                    crc64 = Crc64.Compute(buffer, 0, buffer.Length, crc64);
                }
                return crc64;
            }
            catch
            {
                return null;
            }
        }

        protected ulong? _getRespCrc(TeaResponse response, bool hasRange, object listener, object tracker)
        {
            if (hasRange)
            {
                string crc = DictUtils.GetDicValue(response.Headers, "x-oss-hash-crc64ecma");
                if (string.IsNullOrWhiteSpace(crc))
                {
                    return null;
                }
                else
                {
                    return Convert.ToUInt64(crc);
                }
            }
            return null;
        }

        public void _appendUserAgent(string userAgent)
        {
            if (!string.IsNullOrWhiteSpace(userAgent))
            {
                this._userAgent += " " + userAgent;
            }
        }

        public void _setUserAgent(string userAgent)
        {
            this._userAgent = userAgent;
        }

        public string _getUserAgent()
        {
            if (!string.IsNullOrWhiteSpace(this._userAgent))
            {
                return _defaultUserAgent + " " + this._userAgent;
            }
            return _defaultUserAgent;
        }

        protected Stream _isUploadSpeedLimit(object a, int b)
        {
            throw new Exception("the method is un-implemented!");
        }

        protected bool _ifRange(TeaModel model)
        {
            return !string.IsNullOrEmpty(DictUtils.GetDicValue(model.ToMap(), "Range").ToSafeString());
        }

        protected object _getTracker()
        {
            throw new Exception("the method is un-implemented!");
        }

        protected Dictionary<string, object> _getErrMessage(TeaResponse teaResponse)
        {
            string body = TeaCore.GetResponseBody(teaResponse);
            return (Dictionary<string, object>) DictUtils.GetDicValue(XmlUtil.DeserializeXml(body, typeof(ServiceError)), "Error");
        }

        protected Dictionary<string, string> _toHeader(Dictionary<string, object> headers)
        {
            Dictionary<string, string> dicHeaders = new Dictionary<string, string>();
            foreach (KeyValuePair<string, object> item in headers)
            {
                if (item.Value != null)
                {
                    dicHeaders.Add(item.Key, item.Value.ToString());
                }
            }
            return dicHeaders;
        }

        protected Stream _readAsStream(TeaResponse teaResponse)
        {
            if (teaResponse._Response != null)
            {
                return teaResponse._Response.GetResponseStream();
            }
            return null;
        }

        internal string GetDefaultUserAgent()
        {
            string defaultUserAgent = string.Empty;
            string OSVersion = Environment.OSVersion.ToString();
            string ClientVersion = GetRuntimeRegexValue(RuntimeEnvironment.GetRuntimeDirectory());
            string CoreVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            defaultUserAgent = "Alibaba Cloud (" + OSVersion + ") ";
            defaultUserAgent += ClientVersion;
            defaultUserAgent += " Core/" + CoreVersion;
            return defaultUserAgent;
        }

        internal string GetRuntimeRegexValue(string value)
        {
            var rx = new Regex(@"(\.NET).*(\\|\/).*(\d)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var matches = rx.Match(value);
            char[] separator = { '\\', '/' };

            if (matches.Success)
            {
                var clientValueArray = matches.Value.Split(separator);
                return BuildClientVersion(clientValueArray);
            }

            return "RuntimeNotFound";
        }

        internal string BuildClientVersion(string[] value)
        {
            var finalValue = "";
            for (var i = 0; i < value.Length - 1; ++i)
            {
                finalValue += value[i].Replace(".", "").ToLower();
            }

            finalValue += "/" + value[value.Length - 1];

            return finalValue;
        }

        internal string GetSignatureV1(TeaRequest teaRequest, string bucketName, string accessKeyId, string accessKeySecret)
        {
            string resource = string.Empty;
            if (!string.IsNullOrWhiteSpace(bucketName))
            {
                resource = "/" + bucketName;
            }

            resource = resource + teaRequest.Pathname;

            if (teaRequest.Query.Count > 0 && !resource.Contains('?'))
            {
                resource += "?";
            }

            foreach (var keypair in teaRequest.Query)
            {
                if (StaticConst.signKeyList.Contains(keypair.Key) && !string.IsNullOrWhiteSpace(keypair.Value))
                {
                    if (resource.EndsWith("?"))
                    {
                        resource = resource + keypair.Key + "=" + keypair.Value;
                    }
                    else
                    {
                        resource = resource + "&" + keypair.Key + "=" + keypair.Value;
                    }
                }
            }

            return GetSignedStrV1(teaRequest, resource, accessKeyId, accessKeySecret);
        }

        internal string GetSignedStrV1(TeaRequest teaRequest, string canonicalizedResource, string accessKeyId, string accessKeySecret)
        {
            Dictionary<string, string> temp = new Dictionary<string, string>();

            foreach (var keypair in teaRequest.Headers)
            {
                if (keypair.Key.ToLower().StartsWith("x-oss-"))
                {
                    temp.Add(keypair.Key.ToLower(), keypair.Value);
                }
            }

            Dictionary<string, string> hs = (from dic in temp orderby dic.Key ascending select dic).ToDictionary(p => p.Key, o => o.Value);
            string canonicalizedOSSHeaders = string.Empty;
            foreach (var keypair in hs)
            {
                canonicalizedOSSHeaders += keypair.Key + ":" + keypair.Value + "\n";
            }
            string date = DictUtils.GetDicValue(teaRequest.Headers, "date");
            string contentType = DictUtils.GetDicValue(teaRequest.Headers, "content-type");
            string contentMd5 = DictUtils.GetDicValue(teaRequest.Headers, "content-md5");
            string signStr = teaRequest.Method + "\n" + contentMd5 + "\n" + contentType + "\n" + date + "\n" + canonicalizedOSSHeaders + canonicalizedResource;
            byte[] signData;
            using(KeyedHashAlgorithm algorithm = CryptoConfig.CreateFromName("HMACSHA1") as KeyedHashAlgorithm)
            {
                algorithm.Key = Encoding.UTF8.GetBytes(accessKeySecret);
                signData = algorithm.ComputeHash(Encoding.UTF8.GetBytes(signStr.ToCharArray()));
            }
            string signedStr = Convert.ToBase64String(signData);

            return "OSS " + accessKeyId + ":" + signedStr;
        }

        internal string GetSignatureV2(TeaRequest teaRequest, string bucketName, string accessKeyId, string accessKeySecret, string[] additionalHeaders)
        {
            string resource = string.Empty;
            string pathName = teaRequest.Pathname;
            if (!string.IsNullOrWhiteSpace(bucketName))
            {
                pathName = "/" + bucketName + pathName;
            }

            Dictionary<string, string> sortDict = new Dictionary<string, string>(teaRequest.Query);
            string[] strs = pathName.Split('?');
            resource += UriEncode(strs[0], true);
            if (strs.Length > 1 && !string.IsNullOrWhiteSpace(strs[1]))
            {
                sortDict.Add(strs[1], string.Empty);
            }

            Dictionary<string, string> hs = new Dictionary<string, string>();

            hs = (from dic in sortDict orderby dic.Key ascending select dic).ToDictionary(p => p.Key, o => o.Value);

            if (hs.Count > 0 && !resource.Contains("?"))
            {
                resource += "?";
            }
            foreach (var keypair in hs)
            {
                if (!resource.EndsWith("?"))
                {
                    resource += "&";
                }
                if (!string.IsNullOrEmpty(keypair.Value))
                {
                    resource += UriEncode(keypair.Key, true) + "=" + UriEncode(keypair.Value, true);
                }
                else
                {
                    resource += UriEncode(keypair.Key, true);
                }
            }

            return GetSignedStrV2(teaRequest, resource, accessKeyId, accessKeySecret, additionalHeaders);
        }

        internal string GetSignedStrV2(TeaRequest teaRequest, string canonicalizedResource, string accessKeyId, string accessKeySecret, string[] additionalHeaders)
        {
            Dictionary<string, string> temp = new Dictionary<string, string>();

            foreach (var keypair in teaRequest.Headers)
            {
                if ((from k in additionalHeaders where k.ToLower() == keypair.Key.ToLower() select k).ToList().Count() > 0)
                {
                    temp.Add(keypair.Key.ToLower(), keypair.Value);
                }
                else if (keypair.Key.ToLower().StartsWith("x-oss-"))
                {
                    temp.Add(keypair.Key.ToLower(), keypair.Value);
                }
            }

            Dictionary<string, string> hs = (from dic in temp orderby dic.Key ascending select dic).ToDictionary(p => p.Key, o => o.Value);
            string canonicalizedOSSHeaders = string.Empty;
            foreach (var keypair in hs)
            {
                canonicalizedOSSHeaders += keypair.Key + ":" + keypair.Value + "\n";
            }
            string date = DictUtils.GetDicValue(teaRequest.Headers, "date");
            string contentType = DictUtils.GetDicValue(teaRequest.Headers, "content-type");
            string contentMd5 = DictUtils.GetDicValue(teaRequest.Headers, "content-md5");
            string signStr = teaRequest.Method + "\n" + contentMd5 + "\n" + contentType + "\n" + date + "\n" + canonicalizedOSSHeaders + string.Join(";", additionalHeaders) + "\n" + canonicalizedResource;
            byte[] signData;
            using(KeyedHashAlgorithm algorithm = CryptoConfig.CreateFromName("HMACSHA256") as KeyedHashAlgorithm)
            {
                algorithm.Key = Encoding.UTF8.GetBytes(accessKeySecret);
                signData = algorithm.ComputeHash(Encoding.UTF8.GetBytes(signStr.ToCharArray()));
            }
            string signedStr = Convert.ToBase64String(signData);
            if (additionalHeaders.Length == 0)
            {
                return "OSS2 AccessKeyId:" + accessKeyId + ",Signature:" + signedStr;
            }
            else
            {
                return "OSS2 AccessKeyId:" + accessKeyId + ",AdditionalHeaders:" + string.Join(";", additionalHeaders) + ",Signature:" + signedStr;
            }
        }

        internal string UriEncode(string rawStr, bool encodeSlash)
        {
            string res = string.Empty;
            foreach (char ch in rawStr)
            {
                if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') ||
                    (ch >= '0' && ch <= '9') || characters.Contains(ch))
                {
                    res += ch;
                }
                else if (ch == '/')
                {
                    if (encodeSlash)
                    {
                        res += "%2F";
                    }
                    else
                    {
                        res += ch;
                    }
                }
                else
                {
                    res = res + "%" + ((int) ch).ToString("X2");
                }
            }
            return res;
        }

    }
}