// Shader created with Shader Forge v1.18 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.18;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:14,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:4013,x:32982,y:32660,varname:node_4013,prsc:2|diff-939-OUT,emission-150-OUT;n:type:ShaderForge.SFN_Add,id:3903,x:32090,y:33015,varname:node_3903,prsc:2|A-7343-OUT,B-6658-OUT;n:type:ShaderForge.SFN_Tex2d,id:4757,x:32481,y:32968,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_4757,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a101c8a8b7d21854481972e30af34b7f,ntxv:0,isnm:False|UVIN-672-OUT;n:type:ShaderForge.SFN_Time,id:7719,x:31678,y:32990,varname:node_7719,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:2983,x:30968,y:32824,varname:node_2983,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:6658,x:31889,y:33015,varname:node_6658,prsc:2|A-7719-T,B-9035-OUT;n:type:ShaderForge.SFN_Append,id:672,x:32283,y:32985,varname:node_672,prsc:2|A-2983-U,B-3903-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9035,x:31689,y:33192,ptovrint:False,ptlb:SpeedSlide,ptin:_SpeedSlide,varname:node_9035,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Cos,id:7656,x:32267,y:32702,varname:node_7656,prsc:2|IN-4968-OUT;n:type:ShaderForge.SFN_Pi,id:8225,x:31003,y:32530,varname:node_8225,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4968,x:32106,y:32702,varname:node_4968,prsc:2|A-8225-OUT,B-2983-V;n:type:ShaderForge.SFN_Add,id:939,x:32751,y:32850,varname:node_939,prsc:2|A-1788-OUT,B-4757-RGB;n:type:ShaderForge.SFN_Multiply,id:1788,x:32535,y:32669,varname:node_1788,prsc:2|A-975-OUT,B-7656-OUT;n:type:ShaderForge.SFN_ValueProperty,id:975,x:32300,y:32605,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:node_975,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Clamp01,id:150,x:32751,y:32648,varname:node_150,prsc:2|IN-1788-OUT;n:type:ShaderForge.SFN_Multiply,id:7343,x:31902,y:32852,varname:node_7343,prsc:2|A-312-OUT,B-9909-OUT;n:type:ShaderForge.SFN_Vector1,id:9909,x:31678,y:32901,varname:node_9909,prsc:2,v1:-5;n:type:ShaderForge.SFN_Vector1,id:4494,x:31125,y:32439,varname:node_4494,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Cos,id:312,x:31572,y:32667,varname:node_312,prsc:2|IN-8938-OUT;n:type:ShaderForge.SFN_Multiply,id:8938,x:31381,y:32667,varname:node_8938,prsc:2|A-4494-OUT,B-2983-V,C-8225-OUT;proporder:4757-9035-975;pass:END;sub:END;*/

Shader "Shader Forge/WaterfallMoving" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _SpeedSlide ("SpeedSlide", Float ) = 0.5
        _Intensity ("Intensity", Float ) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            ColorMask RGB
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _SpeedSlide;
            uniform float _Intensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float node_8225 = 3.141592654;
                float node_1788 = (_Intensity*cos((node_8225*i.uv0.g)));
                float4 node_7719 = _Time + _TimeEditor;
                float2 node_672 = float2(i.uv0.r,((cos((0.5*i.uv0.g*node_8225))*(-5.0))+(node_7719.g*_SpeedSlide)));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_672, _MainTex));
                float3 diffuseColor = (node_1788+_MainTex_var.rgb);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float node_150 = saturate(node_1788);
                float3 emissive = float3(node_150,node_150,node_150);
/// Final Color:
                float3 finalColor = diffuse + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            ColorMask RGB
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _SpeedSlide;
            uniform float _Intensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float node_8225 = 3.141592654;
                float node_1788 = (_Intensity*cos((node_8225*i.uv0.g)));
                float4 node_7719 = _Time + _TimeEditor;
                float2 node_672 = float2(i.uv0.r,((cos((0.5*i.uv0.g*node_8225))*(-5.0))+(node_7719.g*_SpeedSlide)));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_672, _MainTex));
                float3 diffuseColor = (node_1788+_MainTex_var.rgb);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
