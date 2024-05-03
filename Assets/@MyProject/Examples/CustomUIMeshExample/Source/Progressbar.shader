// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)
// very simple edit to turn into progressbar by @Minionsart
Shader "UI/ProgressBar"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _ColorMap ("Color Map", 2D) = "white" {}   
        _FillSlider("Fill Slider", Range(0,1)) = 1.0
        _Edge("Edge", Range(0,1)) = 1.0
        _Color ("Edge Tint", Color) = (1,1,1,1)

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            Name "Default"
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                float2 texcoord2  : TEXCOORD2;
                float4 worldPosition : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex, _ColorMap;
            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;
            float _FillSlider;
            float _Edge;

            v2f vert(appdata_t v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.worldPosition = v.vertex;
                OUT.vertex = UnityObjectToClipPos(v.vertex);

                OUT.texcoord = v.texcoord;
                OUT.texcoord2 = TRANSFORM_TEX(v.texcoord, _MainTex);
                OUT.color = v.color ;//* _Color;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                // grab the main color as a base (could be a noise texture for fun results)
                half4 color = (tex2D(_MainTex, IN.texcoord2) + _TextureSampleAdd) * IN.color;
                // use the main texture to colorize the whole thing
                half4 colorMap = tex2D(_ColorMap, color);
                #ifdef UNITY_UI_CLIP_RECT
                    colorMap.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif
                // comment out the sin version and just us the _FillSlider for manual control
                float fillAmount = _FillSlider;
                // float fillAmount = sin(_Time.y * _FillSlider);
                // base bar
                float cutoffpoint =step( IN.texcoord.y , fillAmount- _Edge);
                colorMap *= cutoffpoint;
                // create an edge at the end
                float endbit = step( IN.texcoord.y , fillAmount ) - cutoffpoint;
                colorMap.a += endbit;
                // clipping
                #ifdef UNITY_UI_ALPHACLIP
                    clip (colorMap.a - 0.001);
                #endif
                // add colored edge
                colorMap.rgb += saturate(endbit) * _Color;
                

                return colorMap;
            }
            ENDCG
        }
    }
}