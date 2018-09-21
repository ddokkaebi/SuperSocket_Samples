﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSBaseLib
{
    // 0 ~ 9999
    public enum ERROR_CODE : short
    {
        NONE                        = 0, // 에러가 아니다

        // 서버 초기화 에라
        REDIS_INIT_FAIL             = 1,    // Redis 초기화 에러

        // 로그인 
        LOGIN_INVALID_AUTHTOKEN             = 1001, // 로그인 실패: 잘못된 인증 토큰
        ADD_USER_DUPLICATION                = 1002,
        REMOVE_USER_SEARCH_FAILURE_USER_ID  = 1003,
        USER_AUTH_SEARCH_FAILURE_USER_ID    = 1004,
        USER_AUTH_ALREADY_SET_AUTH          = 1005,

        DB_LOGIN_INVALID_PASSWORD   = 1011,
        DB_LOGIN_EMPTY_USER         = 1012,
        DB_LOGIN_EXCEPTION          = 1013,
    }

    // 1 ~ 10000
    public enum PACKETID : int
    {
        REQ_TEST_ECHO = 1,
        RES_TEST_ECHO = 2,

        NTF_IN_CONNECT_CLIENT       = 11,
        NTF_IN_DISCONNECT_CLIENT    = 12,

        REQ_SS_SERVERINFO   = 31,
        RES_SS_SERVERINFO   = 32,


        // 클라이언트
        CS_BEGIN        = 1001,

        REQ_LOGIN       = 1002,
        RES_LOGIN       = 1003,

        REQ_LOGOUT      = 1011,
        RES_LOGOUT      = 1012,

        CS_END          = 7777,


        // 서버 - 서버
        // DB 8001 ~ 9000
        REQ_DB_LOGIN        = 8101,
        RES_DB_LOGIN        = 8102,
    }

    
    
}