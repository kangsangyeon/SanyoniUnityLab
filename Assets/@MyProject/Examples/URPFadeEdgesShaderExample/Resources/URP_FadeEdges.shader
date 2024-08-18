Shader "Custom/URP_FadeEdges"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        [HideInInspector]_MainTex ("Texture", 2D) = "white" {}
        _Alpha("Alpha", Range(0, 1)) = 1
        _AdditionalBrightness("Additional Brightness", Range(0, 10)) = 0
        _FadeLeft("Fade Left", Range(-1, 0)) = 0
        _FadeRight("Fade Right", Range(-1, 0)) = 0
        _FadeTop("Fade Top", Range(-1, 0)) = 0
        _FadeBottom("Fade Bottom", Range(-1, 0)) = 0
        _HorizontalFadeMul("Horizontal Fade Mul", Float) = 20
        _VerticalFadeMul("Vertical Fade Mul", Float) = 20
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Transparent"
            "Queue"="Transparent"
        }

        Blend SrcAlpha One
        ZWrite Off
        Cull Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)
            float4 _MainTex_ST;
            half4 _Color;
            half _Alpha;
            half _AdditionalBrightness;
            half _FadeLeft;
            half _FadeRight;
            half _FadeTop;
            half _FadeBottom;
            half _HorizontalFadeMul;
            half _VerticalFadeMul;
            CBUFFER_END

            Varyings vert(Attributes i)
            {
                Varyings o;
                o.positionCS = TransformObjectToHClip(i.positionOS.xyz);
                o.uv = TRANSFORM_TEX(i.uv, _MainTex);
                o.screenPos = ComputeScreenPos(o.positionCS);
                return o;
            }

            half4 frag(Varyings i) : SV_Target
            {
                half3 ScreenUV = i.screenPos.xyz / i.screenPos.w;

                half LeftMask = saturate((ScreenUV.x + _FadeLeft) * _HorizontalFadeMul);
                half RightMask = saturate((1 - ScreenUV.x + _FadeRight) * _HorizontalFadeMul);
                half UpMask = saturate((1 - ScreenUV.y + _FadeTop) * _VerticalFadeMul);
                half DownMask = saturate((ScreenUV.y + _FadeBottom) * _VerticalFadeMul);

                half4 c = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv) * _Color;
                c *= (1 + _AdditionalBrightness);
                c.a *= _Alpha * LeftMask * RightMask * DownMask * UpMask;
                c = saturate(c);

                return c;
            }
            ENDHLSL
        }
    }
}