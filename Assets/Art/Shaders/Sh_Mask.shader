Shader "Custom/Biomedical/CustomStencilMask"
{
    Properties
    {
        [IntRange] _StencilRef("Stencil Reference Value", Range(0,255)) = 0
    }
    SubShader {
        Tags {
            "Queue" = "AlphaTest-100"
            "RenderType" = "AlphaTest"
        }

        Pass {
            Name "Mask"
            Cull Off
            ZTest[_ZTest]
            ZWrite Off
            ColorMask 0

            Stencil {
              Ref[_StencilRef]
              Pass Replace
            }
        }
    }
}
