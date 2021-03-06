﻿using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CreateNextScore))]
public class CreateNextScoreEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var obj = target as CreateNextScore;

        //スライダーの表示
        obj.invokeTime = EditorGUILayout.Slider( "InvokeTime", obj.invokeTime, 0.0f, 10.0f );

        //Floatの説明文字と変数の数値表示
        obj.randRange.minValue = EditorGUILayout.FloatField ("RandRangeMin", obj.randRange.minValue);
        obj.randRange.maxValue = EditorGUILayout.FloatField ("RandRangeMax", obj.randRange.maxValue);

        //１メモリの数値を100にする
        obj.randRange.minValue = (int)obj.randRange.minValue / 100 * 100;
        obj.randRange.maxValue = (int)obj.randRange.maxValue / 100 * 100;

        //最大値、最小値を持つスライダーを表示
        EditorGUILayout.MinMaxSlider (ref obj.randRange.minValue, ref obj.randRange.maxValue, 0, 100000);

        //GUI.changedをチェックすることで、いずれかの値が変更された場合、EditorUtility.SetDirty コードが実行されます。
        if (GUI.changed) EditorUtility.SetDirty(target);
    }
}