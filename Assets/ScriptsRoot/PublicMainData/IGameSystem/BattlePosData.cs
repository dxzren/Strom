using UnityEngine;
using System.Collections;
///-------------------------------------------------------------------------------------------------------------------------/// <summary> 战斗位置数据 </summary>
public class BattlePosData
{
    public X_Axis                   XPos                            = X_Axis.None;                                          /// 横向排位
    public Z_Axis                   ZPos                            = Z_Axis.None;                                          /// 竖向排位
    public PosNumType               FixedPosNum                     = PosNumType.Null;                                      /// 固定位置
    public PosNumType               NowPosNum                       = PosNumType.Null;                                      /// 现在位置
    public Vector3                  NowPosV3                        = Vector3.zero;                                         /// 现在世界坐标
    public                          BattlePosData       ()          { }                                                     // 构造函数(1)
    public                          BattlePosData       ( int inNum, Battle_Camp inCamp)                                    // 构造函数(2)      
    {
        if      (inCamp == Battle_Camp.Enemy)                                                                               // 敌方站位 转换  
        {
            switch (inNum)
            {
                case 1: XPos = X_Axis.X_04; ZPos = Z_Axis.Z_01; break;
                case 2: XPos = X_Axis.X_04; ZPos = Z_Axis.Z_02; break;
                case 3: XPos = X_Axis.X_04; ZPos = Z_Axis.Z_03; break;

                case 4: XPos = X_Axis.X_05; ZPos = Z_Axis.Z_01; break;
                case 5: XPos = X_Axis.X_05; ZPos = Z_Axis.Z_02; break;
                case 6: XPos = X_Axis.X_05; ZPos = Z_Axis.Z_03; break;

                case 7: XPos = X_Axis.X_06; ZPos = Z_Axis.Z_01; break;
                case 8: XPos = X_Axis.X_06; ZPos = Z_Axis.Z_02; break;
                case 9: XPos = X_Axis.X_06; ZPos = Z_Axis.Z_03; break;
            }
        }
        else if (inCamp == Battle_Camp.Our)                                                                                 // 我方站位 转换  
        {
            switch (inNum)
            {
                case 1: XPos = X_Axis.X_03; ZPos = Z_Axis.Z_01; break;
                case 2: XPos = X_Axis.X_03; ZPos = Z_Axis.Z_02; break;
                case 3: XPos = X_Axis.X_03; ZPos = Z_Axis.Z_03; break;

                case 4: XPos = X_Axis.X_02; ZPos = Z_Axis.Z_01; break;
                case 5: XPos = X_Axis.X_02; ZPos = Z_Axis.Z_02; break;
                case 6: XPos = X_Axis.X_02; ZPos = Z_Axis.Z_03; break;

                case 7: XPos = X_Axis.X_01; ZPos = Z_Axis.Z_01; break;
                case 8: XPos = X_Axis.X_01; ZPos = Z_Axis.Z_02; break;
                case 9: XPos = X_Axis.X_01; ZPos = Z_Axis.Z_03; break;
            }
        }
        SetPosNumType();                                                                                                    // 设置NumType
    }
    public                          BattlePosData       ( PosNumType inPosNum)                                              // 构造函数(3)      
    {
        switch(inPosNum)
        {
            case PosNumType.Front_1_Our:            XPos = X_Axis.X_03;     ZPos = Z_Axis.Z_01;      break;                 /// 前排上 (我方)
            case PosNumType.Front_2_Our:            XPos = X_Axis.X_03;     ZPos = Z_Axis.Z_02;      break;                 /// 前排中 (我方)
            case PosNumType.Front_3_Our:            XPos = X_Axis.X_03;     ZPos = Z_Axis.Z_03;      break;                 /// 前排下 (我方)

            case PosNumType.Middle_1_Our:           XPos = X_Axis.X_02;     ZPos = Z_Axis.Z_01;      break;                 /// 中排上 (我方)
            case PosNumType.Middle_2_Our:           XPos = X_Axis.X_02;     ZPos = Z_Axis.Z_02;      break;                 /// 中排中 (我方)
            case PosNumType.Middle_3_Our:           XPos = X_Axis.X_02;     ZPos = Z_Axis.Z_03;      break;                 /// 中排下 (我方)

            case PosNumType.Back_1_Our:             XPos = X_Axis.X_01;     ZPos = Z_Axis.Z_01;      break;                 /// 后排上 (我方)
            case PosNumType.Back_2_Our:             XPos = X_Axis.X_01;     ZPos = Z_Axis.Z_02;      break;                 /// 后排中 (我方)
            case PosNumType.Back_3_Our:             XPos = X_Axis.X_01;     ZPos = Z_Axis.Z_03;      break;                 /// 后排下 (我方)


            case PosNumType.Front_1_Enemy:          XPos = X_Axis.X_04;     ZPos = Z_Axis.Z_01;      break;                 /// 前排上 (敌方)
            case PosNumType.Front_2_Enemy:          XPos = X_Axis.X_04;     ZPos = Z_Axis.Z_02;      break;                 /// 前排中 (敌方)
            case PosNumType.Front_3_Enemy:          XPos = X_Axis.X_04;     ZPos = Z_Axis.Z_03;      break;                 /// 前排下 (敌方)

            case PosNumType.Middle_1_Enemy:         XPos = X_Axis.X_05;     ZPos = Z_Axis.Z_01;      break;                 /// 中排上 (敌方)
            case PosNumType.Middle_2_Enemy:         XPos = X_Axis.X_05;     ZPos = Z_Axis.Z_02;      break;                 /// 中排中 (敌方)
            case PosNumType.Middle_3_Enemy:         XPos = X_Axis.X_05;     ZPos = Z_Axis.Z_03;      break;                 /// 中排下 (敌方)

            case PosNumType.Back_1_Enemy:           XPos = X_Axis.X_06;     ZPos = Z_Axis.Z_01;      break;                 /// 后排上 (敌方)
            case PosNumType.Back_2_Enemy:           XPos = X_Axis.X_06;     ZPos = Z_Axis.Z_02;      break;                 /// 后排中 (敌方)
            case PosNumType.Back_3_Enemy:           XPos = X_Axis.X_06;     ZPos = Z_Axis.Z_03;      break;                 /// 后排下 (敌方)
        }
        NowPosNum                   = inPosNum;
        FixedPosNum                 = inPosNum;
    }

    public Vector3                  GetWavePosV3        ( BattleType inBattleMap , int inBattleWave )                       // 战斗波次世界坐标  
    {
        if (inBattleMap == BattleType.CheckPoint)                                                                           /// 关卡战斗波次坐标 
        {
            if      (inBattleWave == 0)                                                                                     // 起始出生点        
            {
                Vector3                 TheV3                           = new Vector3(0,-0.13f,0);

                if      ((int)XPos <= 3)                                                                                    /// 我方成员出生点 
                {
                    switch (ZPos)                                                                                           /// 横排坐标
                    {
                        case Z_Axis.Z_01:   TheV3.z                     = 5.0f;         break;
                        case Z_Axis.Z_02:   TheV3.z                     = 7.0f;         break;
                        case Z_Axis.Z_03:   TheV3.z                     = 9.0f;         break;
                    }
                    switch (XPos)                                                                                           /// 竖排坐标
                    {
                        case X_Axis.X_01:   TheV3.x                     = 18.0f;        break;
                        case X_Axis.X_02:   TheV3.x                     = 16.0f;        break;
                        case X_Axis.X_03:   TheV3.x                     = 14.0f;        break;
                    }
                }
                else                                                                                                        /// 敌方成员出生点 
                {
                    switch (ZPos)
                    {
                        case Z_Axis.Z_01:   TheV3.z                     = 5.0f;         break;
                        case Z_Axis.Z_02:   TheV3.z                     = 7.0f;         break;
                        case Z_Axis.Z_03:   TheV3.z                     = 9.0f;         break;
                    }
                    switch (XPos)
                    {
                        case X_Axis.X_04:   TheV3.x                     = -34.0f;       break;
                        case X_Axis.X_05:   TheV3.x                     = -36.0f;       break;
                        case X_Axis.X_06:   TheV3.x                     = -38.0f;       break;
                    }
                }
                return                      TheV3;
            }
            else if (inBattleWave == 1)                                                                                     // 第一波战斗待机点   
            {
                Vector3                     TheV3                       = new Vector3(0, -0.13f, 0);

                switch (ZPos)                                                                                               /// 横排坐标 
                {
                    case Z_Axis.Z_01:   TheV3.z                         = 5.0f;         break;
                    case Z_Axis.Z_02:   TheV3.z                         = 7.0f;         break;
                    case Z_Axis.Z_03:   TheV3.z                         = 9.0f;         break;
                }
                switch (XPos)                                                                                               /// 竖排坐标 
                {
                    case X_Axis.X_01:   TheV3.x                         = 4.0f;         break;
                    case X_Axis.X_02:   TheV3.x                         = 2.0f;         break;
                    case X_Axis.X_03:   TheV3.x                         = 0.0f;         break;
                    case X_Axis.X_04:   TheV3.x                         = -2.0f;        break;
                    case X_Axis.X_05:   TheV3.x                         = -4.0f;        break;
                    case X_Axis.X_06:   TheV3.x                         = -6.0f;        break;
                }
                return                      TheV3;
            }
            else if (inBattleWave == 2)                                                                                     // 第二波战斗待机点   
            {
                Vector3                     TheV3                       = new Vector3(0, -0.13f, 0);

                switch (ZPos)                                                                                               /// 横排坐标 
                {
                    case Z_Axis.Z_01:   TheV3.z                         = 5.0f;         break;
                    case Z_Axis.Z_02:   TheV3.z                         = 7.0f;         break;
                    case Z_Axis.Z_03:   TheV3.z                         = 9.0f;         break;
                }
                switch (XPos)                                                                                               /// 竖排坐标 
                {
                    case X_Axis.X_01:   TheV3.x                         = -12.0f;       break;
                    case X_Axis.X_02:   TheV3.x                         = -14.0f;       break;
                    case X_Axis.X_03:   TheV3.x                         = -16.5f;       break;
                    case X_Axis.X_04:   TheV3.x                         = -18.0f;       break;
                    case X_Axis.X_05:   TheV3.x                         = -20.0f;       break;
                    case X_Axis.X_06:   TheV3.x                         = -22.0f;       break;
                }
                return                      TheV3;
            }
            else if (inBattleWave == 3)                                                                                     // 第二波战斗待机点   
            {
                Vector3                     TheV3                       = new Vector3(0, -0.13f, 0);

                switch (ZPos)                                                                                               /// 横排坐标 
                {
                    case Z_Axis.Z_01:   TheV3.z                         = 5.0f;         break;
                    case Z_Axis.Z_02:   TheV3.z                         = 7.0f;         break;
                    case Z_Axis.Z_03:   TheV3.z                         = 9.0f;         break;
                }
                switch (XPos)                                                                                               /// 竖排坐标 
                {
                    case X_Axis.X_01:   TheV3.x                         = -32.0f;       break;
                    case X_Axis.X_02:   TheV3.x                         = -34.0f;       break;
                    case X_Axis.X_03:   TheV3.x                         = -36.0f;       break;
                    case X_Axis.X_04:   TheV3.x                         = -38.0f;       break;
                    case X_Axis.X_05:   TheV3.x                         = -40.0f;       break;
                    case X_Axis.X_06:   TheV3.x                         = -42.0f;       break;
                }
                return                      TheV3;
            }
        }
        return                      Vector3.zero;
    }

    public void                     SetPosNumType       ( )                                                                 // 设置坐标位置类型) 
    {
        if      ( ZPos == Z_Axis.Z_01)                                                                                      // 上排(Up)
        {
            if      ( XPos == X_Axis.X_03)          FixedPosNum      = PosNumType.Front_1_Our;          
            else if ( XPos == X_Axis.X_02)          FixedPosNum      = PosNumType.Middle_1_Our; 
            else if ( XPos == X_Axis.X_01)          FixedPosNum      = PosNumType.Back_1_Our;                       
            else if ( XPos == X_Axis.X_04)          FixedPosNum      = PosNumType.Front_1_Enemy; 
            else if ( XPos == X_Axis.X_05)          FixedPosNum      = PosNumType.Middle_1_Enemy;  
            else if ( XPos == X_Axis.X_06)          FixedPosNum      = PosNumType.Back_1_Enemy;                           
        }
        else if ( ZPos == Z_Axis.Z_02)                                                                                      // 中排(Middle)
        {
            if      ( XPos == X_Axis.X_03)          FixedPosNum      = PosNumType.Front_2_Our;          
            else if ( XPos == X_Axis.X_02)          FixedPosNum      = PosNumType.Middle_2_Our; 
            else if ( XPos == X_Axis.X_01)          FixedPosNum      = PosNumType.Back_2_Our;                       
            else if ( XPos == X_Axis.X_04)          FixedPosNum      = PosNumType.Front_2_Enemy; 
            else if ( XPos == X_Axis.X_05)          FixedPosNum      = PosNumType.Middle_2_Enemy;  
            else if ( XPos == X_Axis.X_06)          FixedPosNum      = PosNumType.Back_2_Enemy;  
        }
        else if ( ZPos == Z_Axis.Z_03)                                                                                      // 下排(Down)
        {
            if      ( XPos == X_Axis.X_03)          FixedPosNum      = PosNumType.Front_3_Our;          
            else if ( XPos == X_Axis.X_02)          FixedPosNum      = PosNumType.Middle_3_Our; 
            else if ( XPos == X_Axis.X_01)          FixedPosNum      = PosNumType.Back_3_Our;                       
            else if ( XPos == X_Axis.X_04)          FixedPosNum      = PosNumType.Front_3_Enemy; 
            else if ( XPos == X_Axis.X_05)          FixedPosNum      = PosNumType.Middle_3_Enemy;  
            else if ( XPos == X_Axis.X_06)          FixedPosNum      = PosNumType.Back_3_Enemy;  
        }
    }
}
public enum X_Axis                                                                                                          // 横向排位         
{
    None                                = 0,                                                                                // 未设置
    X_01                                = 1,                                                                                // 后排(Our_Back))
    X_02                                = 2,                                                                                // 中排(Our_Middle)
    X_03                                = 3,                                                                                // 前排(Our_Front)
    X_04                                = 4,                                                                                // 前排(Enemy_Front))
    X_05                                = 5,                                                                                // 中排(Enemy_Middle)
    X_06                                = 6,                                                                                // 后排(Enemy_Back)
}
public enum Z_Axis                                                                                                          // 竖向排位         
{
    None                                = 0,                                                                                // 未设置     
    Z_01                                = 1,                                                                                // 上排 (Up)
    Z_02                                = 2,                                                                                // 中排 (Middle)
    Z_03                                = 3,                                                                                // 下排 (Down)
}
public enum PosNumType                                                                                                      // 坐标位置类型     
{
    Null                                = 0,                            /// 设置空位
    Front_1_Our                         = 1,                            /// 前排上 (我方)
    Front_2_Our                         = 2,                            /// 前排中 (我方)
    Front_3_Our                         = 3,                            /// 前排下 (我方)

    Middle_1_Our                        = 4,                            /// 中排上 (我方)
    Middle_2_Our                        = 5,                            /// 中排中 (我方)
    Middle_3_Our                        = 6,                            /// 中排下 (我方)

    Back_1_Our                          = 7,                            /// 后排上 (我方)
    Back_2_Our                          = 8,                            /// 后排中 (我方)
    Back_3_Our                          = 9,                            /// 后排下 (我方)


    Front_1_Enemy                       = 11,                           /// 前排上 (敌方)
    Front_2_Enemy                       = 12,                           /// 前排中 (敌方)
    Front_3_Enemy                       = 13,                           /// 前排下 (敌方)

    Middle_1_Enemy                      = 14,                           /// 中排上 (敌方)
    Middle_2_Enemy                      = 15,                           /// 中排中 (敌方)
    Middle_3_Enemy                      = 16,                           /// 中排下 (敌方)

    Back_1_Enemy                        = 17,                           /// 后排上 (敌方)
    Back_2_Enemy                        = 18,                           /// 后排中 (敌方)
    Back_3_Enemy                        = 19,                           /// 后排下 (敌方)
}
