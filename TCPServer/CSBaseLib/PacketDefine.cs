using System;
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
        
        WRONG_USER                          = 991,

        // 접속
        ADD_USER_DUPLICATION_SESSION        = 1001,
        ADD_USER_FULL                       = 1002,
        REMOVE_USER_SEARCH_FAILURE_USER_ID  = 1003,
        ADD_USER_DUPLICATION_USERID         = 1004,
        
        // 로그인 
        LOGIN_INVALID_AUTHTOKEN             = 1011, // 로그인 실패: 잘못된 인증 토큰
        USER_AUTH_SEARCH_FAILURE_USER_ID    = 1012,
        USER_AUTH_ALREADY_SET_AUTH          = 1014,

        DB_LOGIN_INVALID_AUTHTOKEN      = 1021,
        DB_LOGIN_EMPTY_USER             = 1022,
        DB_LOGIN_EXCEPTION              = 1023,
        DB_LOGIN_INVALID_NICKNAME       = 1024,

        // 로비 입장
        ENTER_LOBBY_ALREADY_ENTERED     = 1031, // 이미 로비에 들어가 있다.
        ENTER_LOBBY_INVALID_LOBBY_ID    = 1032, // 로비를 찾을 수 없다.
        ENTER_LOBBY_LOBBY_FULL          = 1033, // 로비 인원이 다 찼다
        ENTER_LOBBY_RE_CHECK_INVALID_DATA = 1034,

        DB_ENTER_LOBBY_EXCEPTION        = 1036,
        DB_ENTER_LOBBY_ALREADY_ENTERED  = 1037,
        DB_ENTER_LOBBY_DUPLICATION = 1038,

        // 로비 나가기
        LEAVE_LOBBY_DO_NOT_ENTER_LOBBY  = 1046,

        // 채팅
        LOBBY_CHAT_LOBBY_DO_NOT_ENTER_LOBBY  = 1056,
        LOBBY_CHAT_INVALID_LOBBY_ID = 1057,
        LOBBY_CHAT_INVALID_MESSAGE_LENGTH = 1058,

        // 길드 채팅
        GUILD_CHAT_DO_NOT_ENTER_LOBBY     = 1081,
        GUILD_CHAT_INVALID_GUILD_UNIQUE   = 1082,
        GUILD_CHAT_INVALID_MESSAGE_LENGTH = 1083,
        
        // 유저의 길드 정보 로딩
        ALREADY_USER_GUILD_INFO_LOADED          = 1086,
        DB_LOAD_USER_GUILD_INFO_INVALID_USER_ID = 1087,
        DB_LOAD_USER_GUILD_INFO_EXCEPTION       = 1088,
    }

    public enum PACKETID : int
    {
        INVALID         = 0,

        REQ_TEST_ECHO = 1,
        RES_TEST_ECHO = 2,

        SYSTEM_CONNECT_CLIENT       = 11,
        SYSTEM_DISCONNECT_CLIENT    = 12,
        SYSTEM_LOBBY_INFO_UPDATE    = 15,
        SYSTEM_CHECK_USER_STATUS    = 16,
        SYSTEM_BROADCAST_MESSAG     = 17,
        SYSTEM_WRONG_USER           = 18, // 부정 유저 통보
        SYSTEM_UI_UPDATE            = 19,
        SYSTEM_READ_SERVER_MESSAGE  = 21,


        REQ_SS_SERVERINFO   = 31,
        RES_SS_SERVERINFO   = 32,


        // 클라이언트
        CS_BEGIN        = 1001,

        NTF_DIS_CONNECT = 1002,

        REQ_HEART_BEAT  = 1003,
        RES_HEART_BEAT  = 1004,


        REQ_LOGIN       = 1011,
        RES_LOGIN       = 1012,

        REQ_OWN_DISCONNECT = 1016,
        RES_OWN_DISCONNECT = 1017,
        
        REQ_ENTER_LOBBY = 1021,
        RES_ENTER_LOBBY = 1022,

        REQ_LEAVE_LOBBY = 1026,
        RES_LEAVE_LOBBY = 1027,

        REQ_LOBBY_CHAT  = 1031,
        NTF_LOBBY_CHAT  = 1032,

        NTF_NOTIFICATION = 1036,

        REQ_GUILD_CHAT = 1051,
        NTF_GUILD_CHAT = 1052,

        REQ_LOAD_USER_GUILD_INFO = 1056,
        RES_LOAD_USER_GUILD_INFO = 1057,

        CS_END          = 6999,


        // 서버 - 서버
        // DB 7001 ~ 9000
        REQ_EXECUTE_DB_SAVE_STRING_VALUE    = 7011,
        
        REQ_DB_READ_S2S_MESSAGE        = 7016,
        RES_DB_READ_S2S_MESSAGE        = 7017,

        REQ_DB_LOGIN        = 8101,
        RES_DB_LOGIN        = 8102,

        REQ_DB_ENTER_LOBBY  = 8106,
        RES_DB_ENTER_LOBBY  = 8107,
                
        REQ_DB_LEAVE_LOBBY = 8111,
        RES_DB_LEAVE_LOBBY = 8112,

        NTF_DB_OWN_DISCONNECT = 8116,

        NTF_DB_GUILD_CHAT   = 8121,

        REQ_DB_LOAD_USER_GUILD_INFO = 8126,
        RES_DB_LOAD_USER_GUILD_INFO = 8127,
    }


    public enum LEAVE_LOBBY_TYPE : short 
    {
        REQUEST     = 0, // 요청으로 나감
        FAIL_REQUEST_ENTER       = 1, // 에러에 의해 나감
        DISCONNECT  = 2, // 접속이 끊어지면서 나감
    }

    public enum WRONG_USER_TYPE : short
    {
        NONE                    = 0,
        INVALID_PACKET_ID_RANGE = 1,
        INVALID_PACKET_ID       = 2,
    }

    public enum S2S_MESSAGE_TYPE : short
    {
        NONE            = 0,
        NTF             = 1,
        DIS_CONNECT     = 2,
        GUILD_CHAT      = 3,
    }
    
}
