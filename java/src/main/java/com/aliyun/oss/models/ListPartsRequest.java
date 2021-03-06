// This file is auto-generated, don't edit it. Thanks.
package com.aliyun.oss.models;

import com.aliyun.tea.*;

public class ListPartsRequest extends TeaModel {
    @NameInMap("BucketName")
    @Validation(required = true)
    public String bucketName;

    @NameInMap("ObjectName")
    @Validation(required = true)
    public String objectName;

    @NameInMap("Filter")
    @Validation(required = true)
    public ListPartsRequestFilter filter;

    public static class ListPartsRequestFilter extends TeaModel {
        @NameInMap("uploadId")
        @Validation(required = true)
        public String uploadId;

        @NameInMap("max-parts")
        public Integer maxParts;

        @NameInMap("part-number-marker")
        public Integer partNumberMarker;

        @NameInMap("Encoding-type")
        public String encodingType;

    }

}
