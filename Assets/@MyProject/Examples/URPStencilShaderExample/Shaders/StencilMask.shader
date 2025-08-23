Shader "HolographicCard/StencilMask"
{
    Properties
    {
        [IntRange] _StencilRef("Stencil Ref", Range(0, 255)) = 1
    }
    SubShader
    {
        Tags 
        { 
            "RenderType" = "Opaque"
            "Queue" = "Transparent-5"
            "RenderPipeline" = "UniversalPipeline"
        }

        Pass
        {
            Stencil
			{
				Ref[_StencilRef]
				Comp Always
				Pass Replace
                Fail Keep
			}

            ZWrite On
            ColorMask 0

			Tags
			{
				"LightMode" = "UniversalForward"
			}
        }
    }
    Fallback Off
}
