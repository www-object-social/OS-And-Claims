﻿namespace Unit;
public interface IInfomation
{
    public string ISO639_1 { get; }
    public infomation.Network Network { get; }
    public  StandardInternal.unit.infomation.Type Type { get; }
    public event Action Change;
}