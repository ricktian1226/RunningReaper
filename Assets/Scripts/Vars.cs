using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 错误码定义
/// </summary>
public enum ERRORCODE
{
    NOERROR,
}

/// <summary>
/// 符号枚举值定义
/// </summary>
public enum SYMBOL
{
    Unkown,
    VerticalLine,// |
    HorazionalLine,// -
    Nu,//v
    Lambda,//倒v
    Epsilon, //ε
    Flash,//闪电符号
    HandStandFish,//倒立的鱼 :)
    ReverseC,//翻转的c
    LayDownC,//躺下的c :)
    ReversePhi,//翻转的phi
    TrippleNu,//三个v
    //Upsilon,// U
    Zeta,//z
    MAX,
}

public class PointCloudGestureTemplateName
{
    public const string VerticalLine = "PointCloudGestureTemplateVerticalLine";
    public const string HorazionalLine = "PointCloudGestureTemplateHorizonalLine";
    public const string Nu = "PointCloudGestureTemplateNu";
    public const string Lambda = "PointCloudGestureTemplateLambda";
    public const string Epsilon = "PointCloudGestureTemplateEpsilon";
    public const string Flash = "PointCloudGestureTemplateFlash";
    public const string HandStandFish = "PointCloudGestureTemplateHandStandFish";
    public const string ReverseC = "PointCloudGestureTemplateReverseC";
    public const string LayDownC = "PointCloudGestureTemplateLayDownC";
    public const string ReversePhi = "PointCloudGestureTemplateReversePhi";
    public const string TrippleNu = "PointCloudGestureTemplateTrippleNu";
    //public const string Upsilon = "PointCloudGestureTemplateUpsilon";
    public const string Zeta = "PointCloudGestureTemplateZeta";

    //模版名称和模版枚举值的映射关系
    public static Dictionary<string, SYMBOL> ToSymbolDictionary = new Dictionary<string,SYMBOL>{
        {VerticalLine, SYMBOL.VerticalLine},
        {HorazionalLine, SYMBOL.HorazionalLine},
        {Nu, SYMBOL.Nu},
        {Lambda, SYMBOL.Lambda},
        {Epsilon, SYMBOL.Epsilon},
        {Flash, SYMBOL.Flash},
        {HandStandFish, SYMBOL.HandStandFish},
        {ReverseC, SYMBOL.ReverseC},
        {ReversePhi, SYMBOL.ReversePhi},
        {TrippleNu, SYMBOL.TrippleNu},
        {LayDownC, SYMBOL.LayDownC},
        {Zeta, SYMBOL.Zeta},
    };
}

public class OBJECT_TAG
{
    public const string RUNNER = "Runner";
    public const string MONSTER = "Monster";
    public const string OBSTACLE = "Obstacle";
}

public class ANIMATION_NAME
{
    public const string APEAR = "Apear";
    public const string RUN = "Run";
    public const string IDLE = "Idle";
    public const string DIE = "Die";
}