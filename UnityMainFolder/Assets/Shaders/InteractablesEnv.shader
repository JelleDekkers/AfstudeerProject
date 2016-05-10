// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:33003,y:32699,varname:node_4013,prsc:2|diff-4639-OUT,normal-2502-RGB,emission-6850-OUT,olwid-6765-OUT,olcol-5860-RGB;n:type:ShaderForge.SFN_Color,id:4702,x:31930,y:32845,ptovrint:False,ptlb:Color_copy,ptin:_Color_copy,varname:_Color_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2dAsset,id:7977,x:32053,y:32677,ptovrint:False,ptlb:Albedo,ptin:_Albedo,varname:node_8636,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:91f4ab77217564349b6c623ef8692ac0,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2dAsset,id:1186,x:31419,y:33183,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:node_9476,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:8d8478d743c6c1249ae8df92008b1c79,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:5577,x:32321,y:32649,varname:node_3984,prsc:2,tex:91f4ab77217564349b6c623ef8692ac0,ntxv:0,isnm:False|UVIN-3960-UVOUT,TEX-7977-TEX;n:type:ShaderForge.SFN_Multiply,id:3832,x:32321,y:32832,varname:node_3832,prsc:2|A-5577-RGB,B-4702-RGB;n:type:ShaderForge.SFN_Tex2d,id:2502,x:31592,y:33045,varname:node_9844,prsc:2,tex:8d8478d743c6c1249ae8df92008b1c79,ntxv:0,isnm:False|UVIN-3960-UVOUT,TEX-1186-TEX;n:type:ShaderForge.SFN_OneMinus,id:683,x:31810,y:33021,varname:node_683,prsc:2|IN-2502-B;n:type:ShaderForge.SFN_Slider,id:2560,x:31838,y:33463,ptovrint:False,ptlb:Strength Oclusion,ptin:_StrengthOclusion,varname:node_4025,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:650,x:32024,y:33095,varname:node_650,prsc:2|A-683-OUT,B-3165-RGB;n:type:ShaderForge.SFN_Color,id:3165,x:31708,y:33269,ptovrint:False,ptlb:Color GLow,ptin:_ColorGLow,varname:node_7505,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5514706,c2:0.9814401,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:6850,x:32214,y:33066,varname:node_6850,prsc:2|A-650-OUT,B-2560-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6765,x:32626,y:33076,ptovrint:False,ptlb:Outline width,ptin:_Outlinewidth,varname:node_6765,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.025;n:type:ShaderForge.SFN_Fresnel,id:4085,x:32490,y:33365,varname:node_4085,prsc:2|NRM-8655-OUT,EXP-4794-OUT;n:type:ShaderForge.SFN_NormalVector,id:8655,x:32052,y:33235,prsc:2,pt:True;n:type:ShaderForge.SFN_Add,id:4639,x:32772,y:32756,varname:node_4639,prsc:2|A-3832-OUT,B-1707-OUT;n:type:ShaderForge.SFN_Multiply,id:1707,x:32971,y:33376,varname:node_1707,prsc:2|A-5860-RGB,B-400-OUT;n:type:ShaderForge.SFN_Color,id:5860,x:32474,y:33124,ptovrint:False,ptlb:node_5860,ptin:_node_5860,varname:node_5860,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.8455882,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Posterize,id:400,x:32696,y:33394,varname:node_400,prsc:2|IN-4085-OUT,STPS-5798-OUT;n:type:ShaderForge.SFN_Vector1,id:5798,x:32567,y:33567,varname:node_5798,prsc:2,v1:10;n:type:ShaderForge.SFN_Vector1,id:4794,x:32264,y:33510,varname:node_4794,prsc:2,v1:2;n:type:ShaderForge.SFN_TexCoord,id:3960,x:31991,y:32500,varname:node_3960,prsc:2,uv:0;n:type:ShaderForge.SFN_ViewVector,id:9598,x:32190,y:33362,varname:node_9598,prsc:2;proporder:4702-7977-1186-2560-3165-6765-5860;pass:END;sub:END;*/

Shader "Shader Forge/InteractablesEnv" {
    Properties {
        _Color_copy ("Color_copy", Color) = (1,1,1,1)
        _Albedo ("Albedo", 2D) = "white" {}
        _Normal ("Normal", 2D) = "bump" {}
        _StrengthOclusion ("Strength Oclusion", Range(0, 1)) = 1
        _ColorGLow ("Color GLow", Color) = (0.5514706,0.9814401,1,1)
        _Outlinewidth ("Outline width", Float ) = 0.025
        _node_5860 ("node_5860", Color) = (0.8455882,1,1,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "Outline"
            Tags {
            }
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float _Outlinewidth;
            uniform float4 _node_5860;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_FOG_COORDS(0)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = mul(UNITY_MATRIX_MVP, float4(v.vertex.xyz + v.normal*_Outlinewidth,1) );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                return fixed4(_node_5860.rgb,0);
            }
            ENDCG
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
            uniform float4 _Color_copy;
            uniform sampler2D _Albedo; uniform float4 _Albedo_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform float _StrengthOclusion;
            uniform float4 _ColorGLow;
            uniform float4 _node_5860;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 node_9844 = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 normalLocal = node_9844.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 node_3984 = tex2D(_Albedo,TRANSFORM_TEX(i.uv0, _Albedo));
                float node_5798 = 10.0;
                float3 diffuseColor = ((node_3984.rgb*_Color_copy.rgb)+(_node_5860.rgb*floor(pow(1.0-max(0,dot(normalDirection, viewDirection)),2.0) * node_5798) / (node_5798 - 1)));
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = (((1.0 - node_9844.b)*_ColorGLow.rgb)*_StrengthOclusion);
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
            uniform float4 _Color_copy;
            uniform sampler2D _Albedo; uniform float4 _Albedo_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform float _StrengthOclusion;
            uniform float4 _ColorGLow;
            uniform float4 _node_5860;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 node_9844 = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 normalLocal = node_9844.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 node_3984 = tex2D(_Albedo,TRANSFORM_TEX(i.uv0, _Albedo));
                float node_5798 = 10.0;
                float3 diffuseColor = ((node_3984.rgb*_Color_copy.rgb)+(_node_5860.rgb*floor(pow(1.0-max(0,dot(normalDirection, viewDirection)),2.0) * node_5798) / (node_5798 - 1)));
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
