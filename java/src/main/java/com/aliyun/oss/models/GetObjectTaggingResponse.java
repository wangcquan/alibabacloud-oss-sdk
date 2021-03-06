// This file is auto-generated, don't edit it. Thanks.
package com.aliyun.oss.models;

import com.aliyun.tea.*;

public class GetObjectTaggingResponse extends TeaModel {
    @NameInMap("x-oss-request-id")
    @Validation(required = true)
    public String requestId;

    @NameInMap("Tagging")
    @Validation(required = true)
    public GetObjectTaggingResponseTagging tagging;

    public static class GetObjectTaggingResponseTaggingTagSetTag extends TeaModel {
        @NameInMap("Key")
        public String key;

        @NameInMap("Value")
        public String value;

    }

    public static class GetObjectTaggingResponseTaggingTagSet extends TeaModel {
        @NameInMap("Tag")
        public GetObjectTaggingResponseTaggingTagSetTag[] tag;

    }

    public static class GetObjectTaggingResponseTagging extends TeaModel {
        @NameInMap("TagSet")
        @Validation(required = true)
        public GetObjectTaggingResponseTaggingTagSet tagSet;

    }

}
