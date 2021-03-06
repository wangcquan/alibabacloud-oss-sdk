// This file is auto-generated, don't edit it. Thanks.
package com.aliyun.oss.models;

import com.aliyun.tea.*;

public class GetBucketCORSResponse extends TeaModel {
    @NameInMap("x-oss-request-id")
    @Validation(required = true)
    public String requestId;

    @NameInMap("CORSConfiguration")
    @Validation(required = true)
    public GetBucketCORSResponseCORSConfiguration cORSConfiguration;

    public static class GetBucketCORSResponseCORSConfigurationCORSRule extends TeaModel {
        @NameInMap("MaxAgeSeconds")
        public String maxAgeSeconds;

    }

    public static class GetBucketCORSResponseCORSConfiguration extends TeaModel {
        @NameInMap("CORSRule")
        public GetBucketCORSResponseCORSConfigurationCORSRule[] cORSRule;

    }

}
