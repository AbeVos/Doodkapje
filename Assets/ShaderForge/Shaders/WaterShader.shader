// Shader created with Shader Forge v1.18 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.18;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:4013,x:33201,y:32836,varname:node_4013,prsc:2|diff-1102-RGB,spec-1096-OUT;n:type:ShaderForge.SFN_Tex2d,id:1102,x:32068,y:33029,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_1102,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:a101c8a8b7d21854481972e30af34b7f,ntxv:0,isnm:False|UVIN-9425-OUT;n:type:ShaderForge.SFN_TexCoord,id:4357,x:31389,y:32934,varname:node_4357,prsc:2,uv:0;n:type:ShaderForge.SFN_Time,id:9140,x:31242,y:33109,varname:node_9140,prsc:2;n:type:ShaderForge.SFN_Append,id:9425,x:31866,y:33046,varname:node_9425,prsc:2|A-4357-U,B-1355-OUT;n:type:ShaderForge.SFN_Add,id:1355,x:31619,y:33092,varname:node_1355,prsc:2|A-4357-V,B-7061-OUT;n:type:ShaderForge.SFN_Multiply,id:7061,x:31452,y:33156,varname:node_7061,prsc:2|A-9140-T,B-9522-OUT;n:type:ShaderForge.SFN_Vector1,id:9522,x:31242,y:33258,varname:node_9522,prsc:2,v1:-0.1;n:type:ShaderForge.SFN_Vector1,id:8967,x:32034,y:33504,varname:node_8967,prsc:2,v1:50;n:type:ShaderForge.SFN_NormalVector,id:4501,x:32332,y:33554,prsc:2,pt:False;n:type:ShaderForge.SFN_Multiply,id:9574,x:32997,y:33407,varname:node_9574,prsc:2|A-5706-OUT,B-4501-OUT,C-1085-OUT;n:type:ShaderForge.SFN_Vector1,id:1085,x:32617,y:33631,varname:node_1085,prsc:2,v1:2;n:type:ShaderForge.SFN_ViewVector,id:9720,x:32332,y:33409,varname:node_9720,prsc:2;n:type:ShaderForge.SFN_Dot,id:1096,x:32668,y:33377,varname:node_1096,prsc:2,dt:0|A-9720-OUT,B-4501-OUT;n:type:ShaderForge.SFN_Sin,id:7484,x:32070,y:33328,varname:node_7484,prsc:2|IN-7268-OUT;n:type:ShaderForge.SFN_Multiply,id:7268,x:31845,y:33291,varname:node_7268,prsc:2|A-1355-OUT,B-5590-OUT,C-5446-OUT;n:type:ShaderForge.SFN_Vector1,id:5590,x:31472,y:33386,varname:node_5590,prsc:2,v1:20;n:type:ShaderForge.SFN_Pi,id:5446,x:31522,y:33460,varname:node_5446,prsc:2;n:type:ShaderForge.SFN_Divide,id:6012,x:32353,y:33144,varname:node_6012,prsc:2|A-7484-OUT,B-8967-OUT;n:type:ShaderForge.SFN_Add,id:5706,x:32571,y:33174,varname:node_5706,prsc:2|A-6012-OUT,B-4102-OUT;n:type:ShaderForge.SFN_Vector1,id:4102,x:32384,y:33306,varname:node_4102,prsc:2,v1:0.5;proporder:1102;pass:END;sub:END;*/

Shader "Shader Forge/WaterShader" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
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
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float node_1096 = dot(viewDirection,i.normalDir);
                float3 specularColor = float3(node_1096,node_1096,node_1096);
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 node_9140 = _Time + _TimeEditor;
                float node_1355 = (i.uv0.g+(node_9140.g*(-0.1)));
                float2 node_9425 = float2(i.uv0.r,node_1355);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_9425, _MainTex));
                float3 diffuseColor = _MainTex_var.rgb;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
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
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
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
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float node_1096 = dot(viewDirection,i.normalDir);
                float3 specularColor = float3(node_1096,node_1096,node_1096);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 node_9140 = _Time + _TimeEditor;
                float node_1355 = (i.uv0.g+(node_9140.g*(-0.1)));
                float2 node_9425 = float2(i.uv0.r,node_1355);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_9425, _MainTex));
                float3 diffuseColor = _MainTex_var.rgb;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
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
