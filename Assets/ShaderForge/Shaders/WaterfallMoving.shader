// Shader created with Shader Forge v1.18 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.18;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:14,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:4013,x:32719,y:32712,varname:node_4013,prsc:2|diff-4757-RGB,emission-4757-RGB,alpha-5312-OUT;n:type:ShaderForge.SFN_Add,id:3903,x:32309,y:32653,varname:node_3903,prsc:2|A-2983-V,B-6658-OUT;n:type:ShaderForge.SFN_Tex2d,id:4757,x:32507,y:32894,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_4757,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:187f2bfdfe57ea044b651ced25f4aa44,ntxv:0,isnm:False|UVIN-672-OUT;n:type:ShaderForge.SFN_Time,id:7719,x:31865,y:32898,varname:node_7719,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:2983,x:31711,y:32710,varname:node_2983,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:6658,x:32175,y:33022,varname:node_6658,prsc:2|A-7719-T,B-9035-OUT;n:type:ShaderForge.SFN_Append,id:672,x:32309,y:32827,varname:node_672,prsc:2|A-2983-U,B-3903-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9035,x:32014,y:33160,ptovrint:False,ptlb:SpeedSlide,ptin:_SpeedSlide,varname:node_9035,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Cos,id:7656,x:32148,y:32500,varname:node_7656,prsc:2|IN-4968-OUT;n:type:ShaderForge.SFN_Pi,id:8225,x:31710,y:32590,varname:node_8225,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4968,x:31962,y:32696,varname:node_4968,prsc:2|A-8225-OUT,B-2983-V,C-5245-OUT;n:type:ShaderForge.SFN_Add,id:37,x:32438,y:32411,varname:node_37,prsc:2|A-7805-OUT,B-9343-OUT;n:type:ShaderForge.SFN_Vector1,id:7805,x:31817,y:32334,varname:node_7805,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:5312,x:32662,y:32399,varname:node_5312,prsc:2|A-9035-OUT,B-37-OUT,C-4757-A;n:type:ShaderForge.SFN_Vector1,id:5245,x:31710,y:32858,varname:node_5245,prsc:2,v1:2;n:type:ShaderForge.SFN_Vector1,id:1055,x:32115,y:32374,varname:node_1055,prsc:2,v1:-1;n:type:ShaderForge.SFN_Multiply,id:9343,x:32298,y:32374,varname:node_9343,prsc:2|A-1055-OUT,B-7656-OUT;proporder:4757-9035;pass:END;sub:END;*/

Shader "Shader Forge/WaterfallMoving" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _SpeedSlide ("SpeedSlide", Float ) = 0.5
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
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
                float4 node_7719 = _Time + _TimeEditor;
                float2 node_672 = float2(i.uv0.r,(i.uv0.g+(node_7719.g*_SpeedSlide)));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_672, _MainTex));
                float3 diffuseColor = _MainTex_var.rgb;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = _MainTex_var.rgb;
/// Final Color:
                float3 finalColor = diffuse + emissive;
                fixed4 finalRGBA = fixed4(finalColor,(_SpeedSlide*(1.0+((-1.0)*cos((3.141592654*i.uv0.g*2.0))))*_MainTex_var.a));
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
                float4 node_7719 = _Time + _TimeEditor;
                float2 node_672 = float2(i.uv0.r,(i.uv0.g+(node_7719.g*_SpeedSlide)));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_672, _MainTex));
                float3 diffuseColor = _MainTex_var.rgb;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * (_SpeedSlide*(1.0+((-1.0)*cos((3.141592654*i.uv0.g*2.0))))*_MainTex_var.a),0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
