// This file is auto-generated, don't edit it. Thanks.

using System;
using System.Collections.Generic;
using System.IO;

using Tea;

namespace AlibabaCloud.OSS.Models
{
    public class GetBucketResponse : TeaModel {
        [NameInMap("x-oss-request-id")]
        [Validation(Required=true)]
        public string RequestId { get; set; }

        [NameInMap("ListBucketResult")]
        [Validation(Required=true)]
        public GetBucketResponseListBucketResult ListBucketResult { get; set; }
        public class GetBucketResponseListBucketResult : TeaModel {
            [NameInMap("Name")]
            [Validation(Required=false)]
            public string Name { get; set; }
            [NameInMap("Prefix")]
            [Validation(Required=false)]
            public string Prefix { get; set; }
            [NameInMap("Marker")]
            [Validation(Required=false)]
            public string Marker { get; set; }
            [NameInMap("MaxKeys")]
            [Validation(Required=false)]
            public string MaxKeys { get; set; }
            [NameInMap("Delimiter")]
            [Validation(Required=false)]
            public string Delimiter { get; set; }
            [NameInMap("IsTruncated")]
            [Validation(Required=false)]
            public string IsTruncated { get; set; }
            [NameInMap("EncodingType")]
            [Validation(Required=false)]
            public string EncodingType { get; set; }
            [NameInMap("CommonPrefixes")]
            [Validation(Required=false)]
            public string CommonPrefixes { get; set; }
            [NameInMap("Contents")]
            [Validation(Required=false)]
            public List<GetBucketResponseKey> Contents { get; set; }
            public class GetBucketResponseKey : TeaModel {
                    public string Key { get; set; }
                    public string ETag { get; set; }
                    public string LastModified { get; set; }
                    public string Size { get; set; }
                    public string StorageClass { get; set; }
                    public GetBucketResponseKeyOwner Owner { get; set; }
                    public class GetBucketResponseKeyOwner : TeaModel {
                        [NameInMap("ID")]
                        [Validation(Required=false)]
                        public string ID { get; set; }

                        [NameInMap("DisplayName")]
                        [Validation(Required=false)]
                        public string DisplayName { get; set; }

                    }
            }
        };

    }

}
