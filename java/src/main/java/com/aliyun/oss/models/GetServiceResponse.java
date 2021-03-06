// This file is auto-generated, don't edit it. Thanks.
package com.aliyun.oss.models;

import com.aliyun.tea.*;

public class GetServiceResponse extends TeaModel {
    @NameInMap("x-oss-request-id")
    @Validation(required = true)
    public String requestId;

    @NameInMap("ListAllMyBucketsResult")
    @Validation(required = true)
    public GetServiceResponseListAllMyBucketsResult listAllMyBucketsResult;

    public static class GetServiceResponseListAllMyBucketsResultOwner extends TeaModel {
        @NameInMap("ID")
        public String iD;

        @NameInMap("DisplayName")
        public String displayName;

    }

    public static class GetServiceResponseListAllMyBucketsResultBucketsBucket extends TeaModel {
        @NameInMap("Name")
        public String name;

        @NameInMap("CreateDate")
        public String createDate;

        @NameInMap("Location")
        public String location;

        @NameInMap("ExtranetEndpoint")
        public String extranetEndpoint;

        @NameInMap("IntranetEndpoint")
        public String intranetEndpoint;

        @NameInMap("StorageClass")
        public String storageClass;

    }

    public static class GetServiceResponseListAllMyBucketsResultBuckets extends TeaModel {
        @NameInMap("Bucket")
        public GetServiceResponseListAllMyBucketsResultBucketsBucket[] bucket;

    }

    public static class GetServiceResponseListAllMyBucketsResult extends TeaModel {
        @NameInMap("Prefix")
        public String prefix;

        @NameInMap("Marker")
        public String marker;

        @NameInMap("MaxKeys")
        public String maxKeys;

        @NameInMap("IsTruncated")
        public String isTruncated;

        @NameInMap("NextMarker")
        public String nextMarker;

        @NameInMap("Owner")
        @Validation(required = true)
        public GetServiceResponseListAllMyBucketsResultOwner owner;

        @NameInMap("Buckets")
        @Validation(required = true)
        public GetServiceResponseListAllMyBucketsResultBuckets buckets;

    }

}
