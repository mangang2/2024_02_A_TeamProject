﻿//UCTS_Outline.cginc
//Unitychan Toon Shader ver.2.0
//v.2.0.9
//nobuyuki@unity3d.com
//https://github.com/unity3d-jp/UnityChanWARPSTARCustomToonShaderVer2_Project
//(C)Unity Technologies Japan/UCL
// 2018/08/23 N.Kobayashi (Unity Technologies Japan)
// カメラオフセット付きアウトライン（BaseColorライトカラー反映修正版）
// 2017/06/05 PS4対応版
// Ver.2.0.4.3
// 2018/02/05 Outline Tex対応版
// #pragma multi_compile _IS_OUTLINE_CLIPPING_NO _IS_OUTLINE_CLIPPING_YES 
// _IS_OUTLINE_CLIPPING_YESは、Clippigマスクを使用するシェーダーでのみ使用できる. OutlineのブレンドモードにBlend SrcAlpha OneMinusSrcAlphaを追加すること.
//
            uniform float4 _LightColor0;
            uniform float4 _BaseColor;
            //v.2.0.7.5
            uniform float _Unlit_Intensity;
            uniform fixed _Is_Filter_LightColor;
            uniform fixed _Is_LightColor_Outline;
            //v.2.0.5
            uniform float4 _Color;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Outline_Width;
            uniform float _Farthest_Distance;
            uniform float _Nearest_Distance;
            uniform sampler2D _Outline_Sampler; uniform float4 _Outline_Sampler_ST;
            uniform float4 _Outline_Color;
            uniform fixed _Is_BlendBaseColor;
            uniform float _Offset_Z;
            //v2.0.4
            uniform sampler2D _OutlineTex; uniform float4 _OutlineTex_ST;
            uniform fixed _Is_OutlineTex;
            //Baked Normal Texture for Outline
            uniform sampler2D _BakedNormal; uniform float4 _BakedNormal_ST;
            uniform fixed _Is_BakedNormal;
//ADD CODE SADAFUMI
            uniform sampler2D _SHRTex; uniform float4 _SHRTex_ST;
            uniform sampler2D _MEOTex; uniform float4 _MEOTex_ST;
            uniform float _Offset_Z_Bias;
//ADD CODE SADAFUMI     
            
//v.2.0.4
#ifdef _IS_OUTLINE_CLIPPING_YES
            uniform sampler2D _ClippingMask; uniform float4 _ClippingMask_ST;
            uniform float _Clipping_Level;
            uniform fixed _Inverse_Clipping;
            uniform fixed _IsBaseMapAlphaAsClippingMask;
#endif
            struct VertexInput {
                float4 vertex : POSITION;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;

//ADD CODE SADAFUMI
                float4 vertexColor : COLOR;
                
//ADD CODE SADAFUMI
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;

                float3 normal : NORMAL;

                    
                // v.2.0.9
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float3 tangentDir : TEXCOORD2;
                float3 bitangentDir : TEXCOORD3;
//ADD CODE SADAFUMI    
                float4 DebugColor : TEXCOORD4;
//ADD CODE SADAFUMI    
                // v.2.0.9
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };
            float CustomClamp(float x)
            {
                return 0.99 - 0.49 * x;
            }
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.uv0 = v.texcoord0;
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                float2 Set_UV0 = o.uv0;
                float4 _Outline_Sampler_var = tex2Dlod(_Outline_Sampler,float4(TRANSFORM_TEX(Set_UV0, _Outline_Sampler),0.0,0));
                //v.2.0.4.3 baked Normal Texture for Outline
      
#ifdef _USE_NORMALS_BAKEDTOUV
                o.normalDir = UnityObjectToWorldNormal(normalize(float3(v.texcoord1.x,v.texcoord1.y,v.texcoord2.x)));
                //o.normalDir = normalize(float3(v.texcoord1.x,v.texcoord1.y,v.texcoord2.x));
#else
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                //o.normalDir = normalize(v.normal);
#endif
        
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float3x3 tangentTransform = float3x3( o.tangentDir, o.bitangentDir, o.normalDir);
                //UnpackNormal()が使えないので、以下で展開。使うテクスチャはBump指定をしないこと.
                float4 _BakedNormal_var = (tex2Dlod(_BakedNormal,float4(TRANSFORM_TEX(Set_UV0, _BakedNormal),0.0,0)) * 2 - 1);
                float3 _BakedNormalDir = normalize(mul(_BakedNormal_var.rgb, tangentTransform));
                //ここまで.
//ADD CODE SADAFUMI
                float Set_Outline_Width = (_Outline_Width*0.001*smoothstep( _Farthest_Distance, _Nearest_Distance, distance(objPos.rgb,_WorldSpaceCameraPos) )*(_Outline_Sampler_var.rgb * v.vertexColor.r)).r;

                //float Set_Outline_Width = (_Outline_Width * 0.001 * smoothstep(_Farthest_Distance, _Nearest_Distance, distance(objPos.rgb, _WorldSpaceCameraPos)) * _Outline_Sampler_var.rgb).r;
//ADD CODE SADAFUMI
                //v.2.0.7.5
                float4 _ClipCameraPos = mul(UNITY_MATRIX_VP, float4(_WorldSpaceCameraPos.xyz, 1));
                //v.2.0.7
        
                //o.DebugColor.x = 1-CustomClamp(v.vertexColor.g);
                //o.DebugColor.x = CustomClamp(v.vertexColor.g);
                o.DebugColor.x = 1-v.vertexColor.g;
                //o.DebugColor.x = _Offset_Z + (1 - v.vertexColor.g);
                //o.DebugColor.x = 1-v.vertexColor.g;
        
                //o.DebugColor.x = v.vertexColor.g;
                _Offset_Z = _Offset_Z + ((1 - v.vertexColor.g) * 20);
                //_Offset_Z = (_Offset_Z + ((CustomClamp(1-v.vertexColor.g)) * 20));
                
                #if defined(UNITY_REVERSED_Z)
                    //v.2.0.4.2 (DX)
                    _Offset_Z = _Offset_Z * -0.01;
                #else
                    //OpenGL
                    _Offset_Z = _Offset_Z * 0.01;
                #endif
        
        
                float3 normal = v.tangent.xyz;    
                
                Set_Outline_Width = Set_Outline_Width;
//v2.0.4
#ifdef _OUTLINE_NML
                //v.2.0.4.3 baked Normal Texture for Outline
                    
                float4 OutlinePos = lerp(float4(v.vertex.xyz + normal * Set_Outline_Width, 1), float4(v.vertex.xyz + _BakedNormalDir * Set_Outline_Width, 1), _Is_BakedNormal);                    
                o.pos = UnityObjectToClipPos(OutlinePos);
                o.pos = UnityObjectToClipPos(float4(v.vertex.xyz + normal * Set_Outline_Width, 1));
#elif _OUTLINE_POS        
                Set_Outline_Width = Set_Outline_Width*2;
                float signVar = dot(normalize(v.vertex),normal)<0 ? -1 : 1;
                o.pos = UnityObjectToClipPos(float4(v.vertex.xyz + signVar*normalize(v.vertex)*Set_Outline_Width, 1));
#endif
                //v.2.0.7.5
                o.pos.z = o.pos.z + _Offset_Z * _ClipCameraPos.z;
                //o.pos = float4(0, 0, 0, 1);
                //o.pos.z = o.pos.z + (_Offset_Z + (saturate(1 - v.vertexColor.g) * 20)) * _ClipCameraPos.z;
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target{
                UNITY_SETUP_INSTANCE_ID(i);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
                //v.2.0.5
                _Color = _BaseColor;
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                //v.2.0.9
                float3 envLightSource_GradientEquator = unity_AmbientEquator.rgb >0.05 ? unity_AmbientEquator.rgb : half3(0.05,0.05,0.05);
                float3 envLightSource_SkyboxIntensity = max(ShadeSH9(half4(0.0,0.0,0.0,1.0)),ShadeSH9(half4(0.0,-1.0,0.0,1.0))).rgb;
                float3 ambientSkyColor = envLightSource_SkyboxIntensity.rgb>0.0 ? envLightSource_SkyboxIntensity*_Unlit_Intensity : envLightSource_GradientEquator*_Unlit_Intensity;
                //
                float3 lightColor = _LightColor0.rgb >0.05 ? _LightColor0.rgb : ambientSkyColor.rgb;
                float lightColorIntensity = (0.299*lightColor.r + 0.587*lightColor.g + 0.114*lightColor.b);
                lightColor = lightColorIntensity<1 ? lightColor : lightColor/lightColorIntensity;
                lightColor = lerp(half3(1.0,1.0,1.0), lightColor, _Is_LightColor_Outline);
                float2 Set_UV0 = i.uv0;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(Set_UV0, _MainTex));
                float3 Set_BaseColor = _BaseColor.rgb*_MainTex_var.rgb;
                float3 _Is_BlendBaseColor_var = lerp( _Outline_Color.rgb*lightColor, (_Outline_Color.rgb*Set_BaseColor*Set_BaseColor*lightColor), _Is_BlendBaseColor );
        
                //return float4(i.DebugColor.x, i.DebugColor.x, i.DebugColor.x, 1);
//ADD CODE SADAFUMI
#ifdef _USE_MEOMAP
                float4 _MEOTex_var = tex2D(_MEOTex, TRANSFORM_TEX(Set_UV0, _MEOTex));
#endif
//ADD CODE SADAFUMI


//ADD CODE SADAFUMI
#ifdef _USE_MEOMAP
#else
                float3 _OutlineTex_var = tex2D(_OutlineTex, TRANSFORM_TEX(Set_UV0, _OutlineTex));
#endif
//ADD CODE SADAFUMI
                
//v.2.0.7.5
#ifdef _IS_OUTLINE_CLIPPING_NO
                //ADD CODE SADAFUMI
#ifdef _USE_MEOMAP
                float3 Set_Outline_Color = lerp(_Is_BlendBaseColor_var, _MEOTex_var.bbb * _Outline_Color.rgb * lightColor, _Is_OutlineTex);
#else
                float3 Set_Outline_Color = lerp(_Is_BlendBaseColor_var, _OutlineTex_var.rgb * _Outline_Color.rgb * lightColor, _Is_OutlineTex);
#endif
//ADD CODE SADAFUMI
                
                return float4(Set_Outline_Color,1.0);
#elif _IS_OUTLINE_CLIPPING_YES
//ADD CODE SADAFUMI
#ifdef _USE_SHRMAP
                float4 _SHRTex_var = tex2D(_SHRTex, TRANSFORM_TEX(Set_UV0, _SHRTex));
#endif
//ADD CODE SADAFUMI

//ADD CODE SADAFUMI
#ifdef _USE_SHRMAP
                float Set_MainTexAlpha = _MainTex_var.a;
                float _IsBaseMapAlphaAsClippingMask_var = lerp(_SHRTex_var.b, Set_MainTexAlpha, !_USE_SHRMAP);
#else
                float4 _ClippingMask_var = tex2D(_ClippingMask, TRANSFORM_TEX(Set_UV0, _ClippingMask));
                float Set_MainTexAlpha = _MainTex_var.a;
                float _IsBaseMapAlphaAsClippingMask_var = lerp(_ClippingMask_var.r, Set_MainTexAlpha, _IsBaseMapAlphaAsClippingMask);
#endif       
                float _Inverse_Clipping_var = lerp(_IsBaseMapAlphaAsClippingMask_var, (1.0 - _IsBaseMapAlphaAsClippingMask_var), _Inverse_Clipping);
                float Set_Clipping = saturate((_Inverse_Clipping_var - _Clipping_Level));
                clip(Set_Clipping - 0.5);

//ADD CODE SADAFUMI


                //return float4(i.normalDir,1);
#ifdef _USE_MEOMAP
                float4 Set_Outline_Color = lerp(float4(_Is_BlendBaseColor_var, Set_Clipping), float4((_MEOTex_var.bbb * _Outline_Color.rgb * lightColor), Set_Clipping), _Is_OutlineTex);
#else
                float4 Set_Outline_Color = lerp(float4(_Is_BlendBaseColor_var, Set_Clipping), float4((_OutlineTex_var.rgb * _Outline_Color.rgb * lightColor), Set_Clipping), _Is_OutlineTex);

#endif
                
                return Set_Outline_Color;
#endif
            }
// UCTS_Outline.cginc ここまで.
