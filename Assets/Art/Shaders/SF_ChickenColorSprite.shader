// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:1,cusa:True,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:True,tesm:0,olmd:1,culm:2,bsrc:0,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:True,atwp:True,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:1873,x:33229,y:32719,varname:node_1873,prsc:2|emission-3012-OUT,alpha-4615-OUT;n:type:ShaderForge.SFN_Tex2d,id:4805,x:31916,y:32820,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:True,tagnsco:False,tagnrm:False,tex:715ef6a06dfe2ea4ca600d816c017569,ntxv:0,isnm:False;n:type:ShaderForge.SFN_VertexColor,id:5376,x:31916,y:33063,varname:node_5376,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:3319,x:32472,y:32820,ptovrint:False,ptlb:ColorMap,ptin:_ColorMap,varname:node_3319,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:9183330749dfa5b4b810ad052df02d40,ntxv:0,isnm:False|UVIN-6754-OUT;n:type:ShaderForge.SFN_Multiply,id:4871,x:32130,y:33063,varname:node_4871,prsc:2|A-4805-A,B-5376-A;n:type:ShaderForge.SFN_Set,id:1236,x:32298,y:33063,varname:Opacity,prsc:2|IN-4871-OUT;n:type:ShaderForge.SFN_Get,id:4615,x:32761,y:32978,varname:node_4615,prsc:2|IN-1236-OUT;n:type:ShaderForge.SFN_ComponentMask,id:5815,x:32130,y:32820,varname:node_5815,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-4805-RGB;n:type:ShaderForge.SFN_Multiply,id:3012,x:33030,y:32819,varname:node_3012,prsc:2|A-3319-RGB,B-4615-OUT;n:type:ShaderForge.SFN_Power,id:6754,x:32298,y:32820,cmnt:Gamma Correction,varname:node_6754,prsc:2|VAL-5815-OUT,EXP-3705-OUT;n:type:ShaderForge.SFN_Vector1,id:3705,x:32130,y:32988,varname:node_3705,prsc:2,v1:0.4545455;proporder:4805-3319;pass:END;sub:END;*/

Shader "_Custom/Chicken Color Sprite" {
    Properties {
        [PerRendererData]_MainTex ("MainTex", 2D) = "white" {}
        _ColorMap ("ColorMap", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        _Stencil ("Stencil ID", Float) = 0
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilComp ("Stencil Comparison", Float) = 8
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilOpFail ("Stencil Fail Operation", Float) = 0
        _StencilOpZFail ("Stencil Z-Fail Operation", Float) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "CanUseSpriteAtlas"="True"
            "PreviewType"="Plane"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            Stencil {
                Ref [_Stencil]
                ReadMask [_StencilReadMask]
                WriteMask [_StencilWriteMask]
                Comp [_StencilComp]
                Pass [_StencilOp]
                Fail [_StencilOpFail]
                ZFail [_StencilOpZFail]
            }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _ColorMap; uniform float4 _ColorMap_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                #ifdef PIXELSNAP_ON
                    o.pos = UnityPixelSnap(o.pos);
                #endif
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float2 node_5815 = _MainTex_var.rgb.rg;
                float2 node_6754 = pow(node_5815,0.4545455); // Gamma Correction
                float4 _ColorMap_var = tex2D(_ColorMap,TRANSFORM_TEX(node_6754, _ColorMap));
                float Opacity = (_MainTex_var.a*i.vertexColor.a);
                float node_4615 = Opacity;
                float3 emissive = (_ColorMap_var.rgb*node_4615);
                float3 finalColor = emissive;
                return fixed4(finalColor,node_4615);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                #ifdef PIXELSNAP_ON
                    o.pos = UnityPixelSnap(o.pos);
                #endif
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
