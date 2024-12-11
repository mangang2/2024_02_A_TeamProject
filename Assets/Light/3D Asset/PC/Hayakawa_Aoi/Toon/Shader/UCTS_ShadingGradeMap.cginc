﻿//UCTS_ShadingGradeMap.cginc
//Unitychan Toon Shader ver.2.0
//v.2.0.9
//nobuyuki@unity3d.com
//https://github.com/unity3d-jp/UnityChanWARPSTARCustomToonShaderVer2_Project
//(C)Unity Technologies Japan/UCL
//#pragma multi_compile _IS_TRANSCLIPPING_OFF _IS_TRANSCLIPPING_ON

//#pragma multi_compile _IS_PASS_FWDBASE _IS_PASS_FWDDELTA
//#include "UCTS_ShadingGradeMap.cginc"

            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _BaseColor;
            //v.2.0.5
            uniform float4 _Color;
            uniform fixed _Use_BaseAs1st;
            uniform fixed _Use_1stAs2nd;
            //
            uniform fixed _Is_LightColor_Base;
            uniform sampler2D _1st_ShadeMap; uniform float4 _1st_ShadeMap_ST;
            uniform float4 _1st_ShadeColor;
            uniform fixed _Is_LightColor_1st_Shade;
            uniform sampler2D _2nd_ShadeMap; uniform float4 _2nd_ShadeMap_ST;
            uniform float4 _2nd_ShadeColor;
            uniform fixed _Is_LightColor_2nd_Shade;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform fixed _Is_NormalMapToBase;
            uniform fixed _Set_SystemShadowsToBase;
            uniform float _Tweak_SystemShadowsLevel;

//ADD CODE SADAFUMI
#ifdef _USE_SHRMAP
#else
            uniform sampler2D _ShadingGradeMap; uniform float4 _ShadingGradeMap_ST;
#endif
//ADD CODE SADAFUMI
            
            //v.2.0.6
            uniform float _Tweak_ShadingGradeMapLevel;
            uniform fixed _BlurLevelSGM;
            //
            uniform float _1st_ShadeColor_Step;
            uniform float _1st_ShadeColor_Feather;
            uniform float _2nd_ShadeColor_Step;
            uniform float _2nd_ShadeColor_Feather;
            uniform float4 _HighColor;
            uniform sampler2D _HighColor_Tex; uniform float4 _HighColor_Tex_ST;
            uniform fixed _Is_LightColor_HighColor;
            uniform fixed _Is_NormalMapToHighColor;
            uniform float _HighColor_Power;
            uniform fixed _Is_SpecularToHighColor;
            uniform fixed _Is_BlendAddToHiColor;
            uniform fixed _Is_UseTweakHighColorOnShadow;
            uniform float _TweakHighColorOnShadow;
//ADD CODE SADAFUMI
#ifdef _USE_SHRMAP
#else
            uniform sampler2D _Set_HighColorMask; uniform float4 _Set_HighColorMask_ST;
#endif
//ADD CODE SADAFUMI
            uniform float _Tweak_HighColorMaskLevel;
            uniform fixed _RimLight;
            uniform float4 _RimLightColor;
            uniform fixed _Is_LightColor_RimLight;
            uniform fixed _Is_NormalMapToRimLight;
            uniform float _RimLight_Power;
            uniform float _RimLight_InsideMask;
            uniform fixed _RimLight_FeatherOff;
            uniform fixed _LightDirection_MaskOn;
            uniform float _Tweak_LightDirection_MaskLevel;
            uniform fixed _Add_Antipodean_RimLight;
            uniform float4 _Ap_RimLightColor;
            uniform fixed _Is_LightColor_Ap_RimLight;
            uniform float _Ap_RimLight_Power;
            uniform fixed _Ap_RimLight_FeatherOff;

//ADD CODE SADAFUMI
#ifdef _USE_MEOMAP
#else
            uniform sampler2D _Set_RimLightMask; uniform float4 _Set_RimLightMask_ST;
#endif
//ADD CODE SADAFUMI
            
            uniform float _Tweak_RimLightMaskLevel;
            uniform fixed _MatCap;
            uniform sampler2D _MatCap_Sampler; uniform float4 _MatCap_Sampler_ST;
            uniform float4 _MatCapColor;
            uniform fixed _Is_LightColor_MatCap;
            uniform fixed _Is_BlendAddToMatCap;
            uniform float _Tweak_MatCapUV;
            uniform float _Rotate_MatCapUV;
            uniform fixed _Is_NormalMapForMatCap;
            uniform sampler2D _NormalMapForMatCap; uniform float4 _NormalMapForMatCap_ST;
            uniform float _Rotate_NormalMapForMatCapUV;
            uniform fixed _Is_UseTweakMatCapOnShadow;
            uniform float _TweakMatCapOnShadow;
            //MatcapMask
            uniform sampler2D _Set_MatcapMask; uniform float4 _Set_MatcapMask_ST;
            uniform float _Tweak_MatcapMaskLevel;
            //v.2.0.5
            uniform fixed _Is_Ortho;
            //v.2.0.6
            uniform float _CameraRolling_Stabilizer;
            uniform fixed _BlurLevelMatcap;
            uniform fixed _Inverse_MatcapMask;
            uniform float _BumpScale;
            uniform float _BumpScaleMatcap;
            //Emissive
            uniform sampler2D _Emissive_Tex; uniform float4 _Emissive_Tex_ST;
            uniform float4 _Emissive_Color;
            //v.2.0.7
            uniform fixed _Is_ViewCoord_Scroll;
            uniform float _Rotate_EmissiveUV;
            uniform float _Base_Speed;
            uniform float _Scroll_EmissiveU;
            uniform float _Scroll_EmissiveV;
            uniform fixed _Is_PingPong_Base;
            uniform float4 _ColorShift;
            uniform float4 _ViewShift;
            uniform float _ColorShift_Speed;
            uniform fixed _Is_ColorShift;
            uniform fixed _Is_ViewShift;
            uniform float3 emissive;
            // 
            uniform float _Unlit_Intensity;
            //v.2.0.5
            uniform fixed _Is_Filter_HiCutPointLightColor;
            uniform fixed _Is_Filter_LightColor;
            //v.2.0.4.4
            uniform float _StepOffset;
            uniform fixed _Is_BLD;
            uniform float _Offset_X_Axis_BLD;
            uniform float _Offset_Y_Axis_BLD;
            uniform fixed _Inverse_Z_Axis_BLD;
//ADD CODE SADAFUMI
            uniform fixed _Color_Mesh_Off;
            uniform sampler2D _SHRTex; uniform float4 _SHRTex_ST;
            uniform sampler2D _MEOTex; uniform float4 _MEOTex_ST;
            //uniform int _Check_Vertex_Color;
//ADD CODE SADAFUMI            
//v.2.0.4
#ifdef _IS_TRANSCLIPPING_OFF
//
#elif _IS_TRANSCLIPPING_ON
            uniform sampler2D _ClippingMask; uniform float4 _ClippingMask_ST;
            uniform fixed _IsBaseMapAlphaAsClippingMask;
            uniform float _Clipping_Level;
            uniform fixed _Inverse_Clipping;
            uniform float _Tweak_transparency;
#endif

            // UV回転をする関数：RotateUV()
            //float2 rotatedUV = RotateUV(i.uv0, (_angular_Verocity*3.141592654), float2(0.5, 0.5), _Time.g);
            float2 RotateUV(float2 _uv, float _radian, float2 _piv, float _time)
            {
                float RotateUV_ang = _radian;
                float RotateUV_cos = cos(_time*RotateUV_ang);
                float RotateUV_sin = sin(_time*RotateUV_ang);
                return (mul(_uv - _piv, float2x2( RotateUV_cos, -RotateUV_sin, RotateUV_sin, RotateUV_cos)) + _piv);
            }
            //
            fixed3 DecodeLightProbe( fixed3 N ){
            return ShadeSH9(float4(N,1));
            }
            
            uniform float _GI_Intensity;
//v.2.0.4

            struct VertexInput {
                float4 vertex : POSITION;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
//ADD CODE SADAFUMI
                float4 vertexColor : COLOR;

                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;

                float3 normal : NORMAL;
//ADD CODE SADAFUMI
                // v.2.0.9
                UNITY_VERTEX_INPUT_INSTANCE_ID 
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
    
//ADD CODE SADAFUMI
                float4 vertexColor : TEXCOORD10;
//ADD CODE SADAFUMI
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                //v.2.0.7
                float mirrorFlag : TEXCOORD5;
    
                float3 Debug_Outline_Normal : TEXCOORD6;       

                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                // v.2.0.9
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
#ifdef _USE_NORMALS_BAKEDTOUV
                o.normalDir = UnityObjectToWorldNormal(float3(v.texcoord1.x,v.texcoord1.y,v.texcoord2.x));
#else
                o.normalDir = UnityObjectToWorldNormal(v.normal);
#endif
                
                //o.Debug_Outline_Normal.r = dot(float3(v.texcoord1.x, v.texcoord1.y, v.texcoord2.x),v.normal);
                o.Debug_Outline_Normal.r = dot(UnityObjectToWorldNormal(float3(-v.texcoord1.x, -v.texcoord1.y, -v.texcoord2.x)), UnityObjectToWorldNormal(v.normal));
                //o.Debug_Outline_Normal.r = cross(float3(-v.texcoord1.x, -v.texcoord1.y, -v.texcoord2.x), v.normal);
                //o.Debug_Outline_Normal.xyz = v.tangent.xyz;
                o.Debug_Outline_Normal.xyz = float3(v.tangent.x, v.tangent.y, v.tangent.z);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;

                o.pos = UnityObjectToClipPos( v.vertex );
//ADD CODE SADAFUMI
#ifdef _COLOR_MESH_OFF
                o.pos = float4(0, 0, 0, 1);
#endif
//#ifdef _IS_COLOR_MESH_OFF
//                o.pos = float4(0, 0, 0,1);
//#else
//#endif
//ADD CODE SADAFUMI
                //v.2.0.7 鏡の中判定（右手座標系か、左手座標系かの判定）o.mirrorFlag = -1 なら鏡の中.
                float3 crossFwd = cross(UNITY_MATRIX_V[0], UNITY_MATRIX_V[1]);
                o.mirrorFlag = dot(crossFwd, UNITY_MATRIX_V[2]) < 0 ? 1 : -1;
                //o.Debug_Outline_Normal = UnityObjectToWorldNormal(float3(v.texcoord1.x, v.texcoord1.y, v.texcoord2.x));
                //o.Debug_Outline_Normal = float3(v.texcoord1.x, v.texcoord1.y, -v.texcoord2.x);
                //
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o);
                return o;
            }
            float4 frag(VertexOutput i, fixed facing : VFACE) : SV_TARGET {
                UNITY_SETUP_INSTANCE_ID(i);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float2 Set_UV0 = i.uv0;
                //v.2.0.6
                float3 _NormalMap_var = UnpackScaleNormal(tex2D(_NormalMap,TRANSFORM_TEX(Set_UV0, _NormalMap)), _BumpScale);
                float3 normalLocal = _NormalMap_var.rgb;
                //float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 normalDirection = i.normalDir; // Perturbed normals
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(Set_UV0, _MainTex));
        
                float Trans_Alpha = _MainTex_var.a;
//ADD CODE SADAFUMI
#ifdef _USE_SHRMAP
                float4 _SHRTex_var = tex2D(_SHRTex, TRANSFORM_TEX(Set_UV0, _SHRTex));
                //return float4(_SHRTex_var.b, _SHRTex_var.b, _SHRTex_var.b, 1);
#endif
                

//ADD CODE SADAFUMI
//v.2.0.4
#ifdef _IS_TRANSCLIPPING_OFF
//
#elif _IS_TRANSCLIPPING_ON

        
#ifdef _USE_SHRMAP  
                
                float Set_MainTexAlpha = _MainTex_var.a;
                float _IsBaseMapAlphaAsClippingMask_var = lerp(_SHRTex_var.b, Set_MainTexAlpha, _IsBaseMapAlphaAsClippingMask);
#else
                float4 _ClippingMask_var = tex2D(_ClippingMask, TRANSFORM_TEX(Set_UV0, _ClippingMask));
                float Set_MainTexAlpha = _MainTex_var.a;
                float _IsBaseMapAlphaAsClippingMask_var = lerp(_ClippingMask_var.r, Set_MainTexAlpha, _IsBaseMapAlphaAsClippingMask);
#endif
                float _Inverse_Clipping_var = lerp(_IsBaseMapAlphaAsClippingMask_var, (1.0 - _IsBaseMapAlphaAsClippingMask_var), _Inverse_Clipping);
                float Set_Clipping = saturate((_Inverse_Clipping_var - _Clipping_Level));
#ifdef _USE_CLIP
                clip(Set_Clipping - 0.5);  
#endif
            
                //return float4(_IsBaseMapAlphaAsClippingMask_var, _IsBaseMapAlphaAsClippingMask_var, _IsBaseMapAlphaAsClippingMask_var, 1);
                //return float4(_SHRTex_var.b, _SHRTex_var.b, _SHRTex_var.b, 1);
                //return float4(Set_Clipping,Set_Clipping,Set_Clipping,1);
    #ifdef _USE_SHRMAP  
        
                Trans_Alpha = min(_SHRTex_var.b,1-_Clipping_Level);
    #else
                Trans_Alpha = min(Trans_Alpha ,1-_Clipping_Level);
    #endif
#endif
                
                UNITY_LIGHT_ATTENUATION(attenuation, i, i.posWorld.xyz);
//v.2.0.4
#ifdef _IS_PASS_FWDBASE
                float3 defaultLightDirection = normalize(UNITY_MATRIX_V[2].xyz + UNITY_MATRIX_V[1].xyz);
                //v.2.0.5
                float3 defaultLightColor = saturate(max(half3(0.05,0.05,0.05)*_Unlit_Intensity,max(ShadeSH9(half4(0.0, 0.0, 0.0, 1.0)),ShadeSH9(half4(0.0, -1.0, 0.0, 1.0)).rgb)*_Unlit_Intensity));
                float3 customLightDirection = normalize(mul( unity_ObjectToWorld, float4(((float3(1.0,0.0,0.0)*_Offset_X_Axis_BLD*10)+(float3(0.0,1.0,0.0)*_Offset_Y_Axis_BLD*10)+(float3(0.0,0.0,-1.0)*lerp(-1.0,1.0,_Inverse_Z_Axis_BLD))),0)).xyz);
                float3 lightDirection = normalize(lerp(defaultLightDirection,_WorldSpaceLightPos0.xyz,any(_WorldSpaceLightPos0.xyz)));
                lightDirection = lerp(lightDirection, customLightDirection, _Is_BLD);
                //v.2.0.5: 
                float3 lightColor = lerp(max(defaultLightColor,_LightColor0.rgb),max(defaultLightColor,saturate(_LightColor0.rgb)),_Is_Filter_LightColor);
#elif _IS_PASS_FWDDELTA
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                //v.2.0.5: 
                float3 addPassLightColor = (0.5*dot(lerp( i.normalDir, normalDirection, _Is_NormalMapToBase ), lightDirection)+0.5) * _LightColor0.rgb * attenuation;
                float pureIntencity = max(0.001,(0.299*_LightColor0.r + 0.587*_LightColor0.g + 0.114*_LightColor0.b));
                float3 lightColor = max(0, lerp(addPassLightColor, lerp(0,min(addPassLightColor,addPassLightColor/pureIntencity),_WorldSpaceLightPos0.w),_Is_Filter_LightColor));
#endif
////// Lighting:
                float3 halfDirection = normalize(viewDirection+lightDirection);
                //v.2.0.5
                _Color = _BaseColor;

#ifdef _IS_PASS_FWDBASE
                float3 Set_LightColor = lightColor.rgb;
                float3 Set_BaseColor = lerp( (_MainTex_var.rgb*_BaseColor.rgb), ((_MainTex_var.rgb*_BaseColor.rgb)*Set_LightColor), _Is_LightColor_Base );
                //v.2.0.5
                float4 _1st_ShadeMap_var = lerp(tex2D(_1st_ShadeMap,TRANSFORM_TEX(Set_UV0, _1st_ShadeMap)),_MainTex_var,_Use_BaseAs1st);
                float3 _Is_LightColor_1st_Shade_var = lerp( (_1st_ShadeMap_var.rgb*_1st_ShadeColor.rgb), ((_1st_ShadeMap_var.rgb*_1st_ShadeColor.rgb)*Set_LightColor), _Is_LightColor_1st_Shade );
                float _HalfLambert_var = 0.5*dot(lerp( i.normalDir, normalDirection, _Is_NormalMapToBase ),lightDirection)+0.5; // Half Lambert
                //float4 _ShadingGradeMap_var = tex2D(_ShadingGradeMap,TRANSFORM_TEX(Set_UV0, _ShadingGradeMap));
                //v.2.0.6

//ADD CODE SADAFUMI
#ifdef _USE_MEOMAP
                float4 _MEOTex_var = tex2D(_MEOTex, TRANSFORM_TEX(Set_UV0, _MEOTex));
#endif
//ADD CODE SADAFUMI


//ADD CODE SADAFUMI
#ifdef _USE_SHRMAP
#else
                float4 _ShadingGradeMap_var = tex2Dlod(_ShadingGradeMap, float4(TRANSFORM_TEX(Set_UV0, _ShadingGradeMap), 0.0, _BlurLevelSGM));
#endif
//ADD CODE SADAFUMI
                //v.2.0.6
                //Minmimum value is same as the Minimum Feather's value with the Minimum Step's value as threshold.
                float _SystemShadowsLevel_var = (attenuation*0.5)+0.5+_Tweak_SystemShadowsLevel > 0.001 ? (attenuation*0.5)+0.5+_Tweak_SystemShadowsLevel : 0.0001;
   
//ADD CODE SADAFUMI
#ifdef _USE_SHRMAP
                float _ShadingGradeMapLevel_var = _SHRTex_var.r < 0.95 ? _SHRTex_var.r + _Tweak_ShadingGradeMapLevel : 1;
#else
                float _ShadingGradeMapLevel_var = _ShadingGradeMap_var.r < 0.95 ? _ShadingGradeMap_var.r + _Tweak_ShadingGradeMapLevel : 1;
#endif
//ADD CODE SADAFUMI
                
                
                float Set_ShadingGrade = saturate(_ShadingGradeMapLevel_var)*lerp( _HalfLambert_var, (_HalfLambert_var*saturate(_SystemShadowsLevel_var)), _Set_SystemShadowsToBase );
                //
                float Set_FinalShadowMask = saturate((1.0 + ( (Set_ShadingGrade - (_1st_ShadeColor_Step-_1st_ShadeColor_Feather)) * (0.0 - 1.0) ) / (_1st_ShadeColor_Step - (_1st_ShadeColor_Step-_1st_ShadeColor_Feather)))); // Base and 1st Shade Mask
                float3 _BaseColor_var = lerp(Set_BaseColor,_Is_LightColor_1st_Shade_var,Set_FinalShadowMask);
                //v.2.0.5
                float4 _2nd_ShadeMap_var = lerp(tex2D(_2nd_ShadeMap,TRANSFORM_TEX(Set_UV0, _2nd_ShadeMap)),_1st_ShadeMap_var,_Use_1stAs2nd);
                float Set_ShadeShadowMask = saturate((1.0 + ( (Set_ShadingGrade - (_2nd_ShadeColor_Step-_2nd_ShadeColor_Feather)) * (0.0 - 1.0) ) / (_2nd_ShadeColor_Step - (_2nd_ShadeColor_Step-_2nd_ShadeColor_Feather)))); // 1st and 2nd Shades Mask
                //Composition: 3 Basic Colors as Set_FinalBaseColor
                float3 Set_FinalBaseColor = lerp(_BaseColor_var,lerp(_Is_LightColor_1st_Shade_var,lerp( (_2nd_ShadeMap_var.rgb*_2nd_ShadeColor.rgb), ((_2nd_ShadeMap_var.rgb*_2nd_ShadeColor.rgb)*Set_LightColor), _Is_LightColor_2nd_Shade ),Set_ShadeShadowMask),Set_FinalShadowMask);
//ADD CODE SADAFUMI
#ifdef _USE_SHRMAP
#else
                float4 _Set_HighColorMask_var = tex2D(_Set_HighColorMask, TRANSFORM_TEX(Set_UV0, _Set_HighColorMask));
#endif
//ADD CODE SADAFUMI
               
                float _Specular_var = 0.5*dot(halfDirection,lerp( i.normalDir, normalDirection, _Is_NormalMapToHighColor ))+0.5; // Specular

//ADD CODE SADAFUMI
#ifdef _USE_SHRMAP
                float _TweakHighColorMask_var = (saturate((_SHRTex_var.g + _Tweak_HighColorMaskLevel)) * lerp((1.0 - step(_Specular_var, (1.0 - pow(_HighColor_Power, 5)))), pow(_Specular_var, exp2(lerp(11, 1, _HighColor_Power))), _Is_SpecularToHighColor));
#else
                float _TweakHighColorMask_var = (saturate((_Set_HighColorMask_var.g + _Tweak_HighColorMaskLevel)) * lerp((1.0 - step(_Specular_var, (1.0 - pow(_HighColor_Power, 5)))), pow(_Specular_var, exp2(lerp(11, 1, _HighColor_Power))), _Is_SpecularToHighColor));
#endif
//ADD CODE SADAFUMI

                float4 _HighColor_Tex_var = tex2D(_HighColor_Tex,TRANSFORM_TEX(Set_UV0, _HighColor_Tex));
                float3 _HighColor_var = (lerp( (_HighColor_Tex_var.rgb*_HighColor.rgb), ((_HighColor_Tex_var.rgb*_HighColor.rgb)*Set_LightColor), _Is_LightColor_HighColor )*_TweakHighColorMask_var);
                //Composition: 3 Basic Colors and HighColor as Set_HighColor
                float3 Set_HighColor = (lerp( saturate((Set_FinalBaseColor-_TweakHighColorMask_var)), Set_FinalBaseColor, lerp(_Is_BlendAddToHiColor,1.0,_Is_SpecularToHighColor) )+lerp( _HighColor_var, (_HighColor_var*((1.0 - Set_FinalShadowMask)+(Set_FinalShadowMask*_TweakHighColorOnShadow))), _Is_UseTweakHighColorOnShadow ));

//ADD CODE SADAFUMI
#ifdef _USE_MEOMAP
#else
                float4 _Set_RimLightMask_var = tex2D(_Set_RimLightMask, TRANSFORM_TEX(Set_UV0, _Set_RimLightMask));
#endif
//ADD CODE SADAFUMI
                

                float3 _Is_LightColor_RimLight_var = lerp( _RimLightColor.rgb, (_RimLightColor.rgb*Set_LightColor), _Is_LightColor_RimLight );
                float _RimArea_var = (1.0 - dot(lerp( i.normalDir, normalDirection, _Is_NormalMapToRimLight ),viewDirection));
            //ADD CODE SADAFUMI


                float _RimLightPower_var = pow(_RimArea_var,exp2(lerp(3,0,_RimLight_Power)));
            //ADD CODE SADAFUMI
                
         //float _Rimlight_InsideMask_var = saturate(lerp( (0.0 + ( (_RimLightPower_var - _RimLight_InsideMask) * (1.0 - 0.0) ) / (1.0 - _RimLight_InsideMask)), step(_RimLight_InsideMask,_RimLightPower_var), _RimLight_FeatherOff ));
#ifdef _USE_MEOMAP     
         float _Rimlight_InsideMask_var = saturate(lerp( (0.0 + ( (_RimLightPower_var - _RimLight_InsideMask) * (1.0 - 0.0) ) / (1.0 - _RimLight_InsideMask)), step(_RimLight_InsideMask,_RimLightPower_var), _RimLight_FeatherOff ));
#else
         float _Rimlight_InsideMask_var = saturate(lerp( (0.0 + ( (_RimLightPower_var - _RimLight_InsideMask) * (1.0 - 0.0) ) / (1.0 - _RimLight_InsideMask)), step(_RimLight_InsideMask,_RimLightPower_var), _RimLight_FeatherOff ));
#endif
               
                float _VertHalfLambert_var = 0.5*dot(i.normalDir,lightDirection)+0.5;
                float3 _LightDirection_MaskOn_var = lerp( (_Is_LightColor_RimLight_var*_Rimlight_InsideMask_var), (_Is_LightColor_RimLight_var*saturate((_Rimlight_InsideMask_var-((1.0 - _VertHalfLambert_var)+_Tweak_LightDirection_MaskLevel)))), _LightDirection_MaskOn );
                float _ApRimLightPower_var = pow(_RimArea_var,exp2(lerp(3,0,_Ap_RimLight_Power)));
                 
//ADD CODE SADAFUMI
#ifdef _USE_MEOMAP
                float3 Set_RimLight = (saturate(((_MEOTex_var.r * 2) * _Tweak_RimLightMaskLevel)) * lerp(_LightDirection_MaskOn_var, (_LightDirection_MaskOn_var + (lerp(_Ap_RimLightColor.rgb, (_Ap_RimLightColor.rgb * Set_LightColor), _Is_LightColor_Ap_RimLight) * saturate((lerp((0.0 + ((_ApRimLightPower_var - _RimLight_InsideMask) * (1.0 - 0.0)) / (1.0 - _RimLight_InsideMask)), step(_RimLight_InsideMask, _ApRimLightPower_var), _Ap_RimLight_FeatherOff) - (saturate(_VertHalfLambert_var) + _Tweak_LightDirection_MaskLevel))))), _Add_Antipodean_RimLight));
#else
                float3 Set_RimLight = (saturate((_Set_RimLightMask_var.g* _Tweak_RimLightMaskLevel)) * lerp(_LightDirection_MaskOn_var, (_LightDirection_MaskOn_var + (lerp(_Ap_RimLightColor.rgb, (_Ap_RimLightColor.rgb * Set_LightColor), _Is_LightColor_Ap_RimLight) * saturate((lerp((0.0 + ((_ApRimLightPower_var - _RimLight_InsideMask) * (1.0 - 0.0)) / (1.0 - _RimLight_InsideMask)), step(_RimLight_InsideMask, _ApRimLightPower_var), _Ap_RimLight_FeatherOff) - (saturate(_VertHalfLambert_var) + _Tweak_LightDirection_MaskLevel))))), _Add_Antipodean_RimLight));
#endif
//ADD CODE SADAFUMI

                //Composition: HighColor and RimLight as _RimLight_var
                float3 _RimLight_var = lerp( Set_HighColor, (Set_HighColor+Set_RimLight), _RimLight );
                //Matcap
                //v.2.0.6 : CameraRolling Stabilizer
                //鏡スクリプト判定：_sign_Mirror = -1 なら、鏡の中と判定.
                //v.2.0.7
                fixed _sign_Mirror = i.mirrorFlag;
                //
                float3 _Camera_Right = UNITY_MATRIX_V[0].xyz;
                float3 _Camera_Front = UNITY_MATRIX_V[2].xyz;
                float3 _Up_Unit = float3(0, 1, 0);
                float3 _Right_Axis = cross(_Camera_Front, _Up_Unit);
                //鏡の中なら反転.
                if(_sign_Mirror < 0){
                    _Right_Axis = -1 * _Right_Axis;
                    _Rotate_MatCapUV = -1 * _Rotate_MatCapUV;
                }else{
                    _Right_Axis = _Right_Axis;
                }
                float _Camera_Right_Magnitude = sqrt(_Camera_Right.x*_Camera_Right.x + _Camera_Right.y*_Camera_Right.y + _Camera_Right.z*_Camera_Right.z);
                float _Right_Axis_Magnitude = sqrt(_Right_Axis.x*_Right_Axis.x + _Right_Axis.y*_Right_Axis.y + _Right_Axis.z*_Right_Axis.z);
                float _Camera_Roll_Cos = dot(_Right_Axis, _Camera_Right) / (_Right_Axis_Magnitude * _Camera_Right_Magnitude);
                float _Camera_Roll = acos(clamp(_Camera_Roll_Cos, -1, 1));
                fixed _Camera_Dir = _Camera_Right.y < 0 ? -1 : 1;
                float _Rot_MatCapUV_var_ang = (_Rotate_MatCapUV*3.141592654) - _Camera_Dir*_Camera_Roll*_CameraRolling_Stabilizer;
                //v.2.0.7
                float2 _Rot_MatCapNmUV_var = RotateUV(Set_UV0, (_Rotate_NormalMapForMatCapUV*3.141592654), float2(0.5, 0.5), 1.0);
                //V.2.0.6
                float3 _NormalMapForMatCap_var = UnpackScaleNormal(tex2D(_NormalMapForMatCap,TRANSFORM_TEX(_Rot_MatCapNmUV_var, _NormalMapForMatCap)),_BumpScaleMatcap);
                //v.2.0.5: MatCap with camera skew correction
                //float3 viewNormal = (mul(UNITY_MATRIX_V, float4(lerp( i.normalDir, mul( _NormalMapForMatCap_var.rgb, tangentTransform ).rgb, _Is_NormalMapForMatCap ),0))).rgb;
                float3 viewNormal = (mul(UNITY_MATRIX_V, float4( i.normalDir,0))).rgb;
                float3 NormalBlend_MatcapUV_Detail = viewNormal.rgb * float3(-1,-1,1);
                float3 NormalBlend_MatcapUV_Base = (mul( UNITY_MATRIX_V, float4(viewDirection,0) ).rgb*float3(-1,-1,1)) + float3(0,0,1);
                float3 noSknewViewNormal = NormalBlend_MatcapUV_Base*dot(NormalBlend_MatcapUV_Base, NormalBlend_MatcapUV_Detail)/NormalBlend_MatcapUV_Base.b - NormalBlend_MatcapUV_Detail;                
                float2 _ViewNormalAsMatCapUV = (lerp(noSknewViewNormal,viewNormal,_Is_Ortho).rg*0.5)+0.5;
                //
                //v.2.0.7
                float2 _Rot_MatCapUV_var = RotateUV((0.0 + ((_ViewNormalAsMatCapUV - (0.0+_Tweak_MatCapUV)) * (1.0 - 0.0) ) / ((1.0-_Tweak_MatCapUV) - (0.0+_Tweak_MatCapUV))), _Rot_MatCapUV_var_ang, float2(0.5, 0.5), 1.0);
                //鏡の中ならUV左右反転.
                //if(_sign_Mirror < 0){
                //    _Rot_MatCapUV_var.x = 1-_Rot_MatCapUV_var.x;
                //}else{
                //    _Rot_MatCapUV_var = _Rot_MatCapUV_var;
                //}
                //v.2.0.6 : LOD of Matcap
                float4 _MatCap_Sampler_var = tex2Dlod(_MatCap_Sampler,float4(TRANSFORM_TEX(_Rot_MatCapUV_var, _MatCap_Sampler),0.0,_BlurLevelMatcap));
                //                
                //MatcapMask


//ADD CODE SADAFUMI
#ifdef _USE_MEOMAP
#else
                float4 _Set_MatcapMask_var = tex2D(_Set_MatcapMask, TRANSFORM_TEX(Set_UV0, _Set_MatcapMask));
#endif
//ADD CODE SADAFUMI               

//ADD CODE SADAFUMI
#ifdef _USE_MEOMAP
                float _Tweak_MatcapMaskLevel_var = saturate(lerp(_MEOTex_var.g, (1.0 - _MEOTex_var.g), _Inverse_MatcapMask) + _Tweak_MatcapMaskLevel);
#else
                float _Tweak_MatcapMaskLevel_var = saturate(lerp(_Set_MatcapMask_var.g, (1.0 - _Set_MatcapMask_var.g), _Inverse_MatcapMask) + _Tweak_MatcapMaskLevel);
#endif
//ADD CODE SADAFUMI

                float3 _Is_LightColor_MatCap_var = lerp( (_MatCap_Sampler_var.rgb*_MatCapColor.rgb), ((_MatCap_Sampler_var.rgb*_MatCapColor.rgb)*Set_LightColor), _Is_LightColor_MatCap );
                //v.2.0.6 : ShadowMask on Matcap in Blend mode : multiply
                float3 Set_MatCap = 
            lerp( _Is_LightColor_MatCap_var, 
            (_Is_LightColor_MatCap_var*((1.0 - Set_FinalShadowMask)+(Set_FinalShadowMask*_TweakMatCapOnShadow)) +
        lerp(Set_HighColor*Set_FinalShadowMask*(1.0-_TweakMatCapOnShadow), float3(0.0, 0.0, 0.0), _Is_BlendAddToMatCap)), 
        _Is_UseTweakMatCapOnShadow );
                //
                //v.2.0.6
                //Composition: RimLight and MatCap as finalColor
                //Broke down finalColor composition
                float3 matCapColorOnAddMode = _RimLight_var+Set_MatCap*_Tweak_MatcapMaskLevel_var;
                float _Tweak_MatcapMaskLevel_var_MultiplyMode = _Tweak_MatcapMaskLevel_var * lerp (1, (1 - (Set_FinalShadowMask)*(1 - _TweakMatCapOnShadow)), _Is_UseTweakMatCapOnShadow);
                float3 matCapColorOnMultiplyMode = Set_HighColor*(1-_Tweak_MatcapMaskLevel_var_MultiplyMode) + Set_HighColor*Set_MatCap*_Tweak_MatcapMaskLevel_var_MultiplyMode + lerp(float3(0,0,0),Set_RimLight,_RimLight);
                float3 matCapColorFinal = lerp(matCapColorOnMultiplyMode, matCapColorOnAddMode, _Is_BlendAddToMatCap);

                float3 finalColor = lerp(_RimLight_var, matCapColorFinal, _MatCap);// Final Composition before Emissive
                
                //
//v.2.0.7
#ifdef _EMISSIVE_SIMPLE
                float4 _Emissive_Tex_var = tex2D(_Emissive_Tex,TRANSFORM_TEX(Set_UV0, _Emissive_Tex));
                float emissiveMask = _Emissive_Tex_var.a;
                emissive = _Emissive_Tex_var.rgb * _Emissive_Color.rgb * emissiveMask;
#elif _EMISSIVE_ANIMATION
                //v.2.0.7 Calculation View Coord UV for Scroll 
                float3 viewNormal_Emissive = (mul(UNITY_MATRIX_V, float4(i.normalDir,0))).xyz;
                float3 NormalBlend_Emissive_Detail = viewNormal_Emissive * float3(-1,-1,1);
                float3 NormalBlend_Emissive_Base = (mul( UNITY_MATRIX_V, float4(viewDirection,0)).xyz*float3(-1,-1,1)) + float3(0,0,1);
                float3 noSknewViewNormal_Emissive = NormalBlend_Emissive_Base*dot(NormalBlend_Emissive_Base, NormalBlend_Emissive_Detail)/NormalBlend_Emissive_Base.z - NormalBlend_Emissive_Detail;
                float2 _ViewNormalAsEmissiveUV = noSknewViewNormal_Emissive.xy*0.5+0.5;
                float2 _ViewCoord_UV = RotateUV(_ViewNormalAsEmissiveUV, -(_Camera_Dir*_Camera_Roll), float2(0.5,0.5), 1.0);
                //鏡の中ならUV左右反転.
                if(_sign_Mirror < 0){
                    _ViewCoord_UV.x = 1-_ViewCoord_UV.x;
                }else{
                    _ViewCoord_UV = _ViewCoord_UV;
                }
                float2 emissive_uv = lerp(i.uv0, _ViewCoord_UV, _Is_ViewCoord_Scroll);
                //
                float4 _time_var = _Time;
                float _base_Speed_var = (_time_var.g*_Base_Speed);
                float _Is_PingPong_Base_var = lerp(_base_Speed_var, sin(_base_Speed_var), _Is_PingPong_Base );
                float2 scrolledUV = emissive_uv + float2(_Scroll_EmissiveU, _Scroll_EmissiveV)*_Is_PingPong_Base_var;
                float rotateVelocity = _Rotate_EmissiveUV*3.141592654;
                float2 _rotate_EmissiveUV_var = RotateUV(scrolledUV, rotateVelocity, float2(0.5, 0.5), _Is_PingPong_Base_var);
                float4 _Emissive_Tex_var = tex2D(_Emissive_Tex,TRANSFORM_TEX(Set_UV0, _Emissive_Tex));
                float emissiveMask = _Emissive_Tex_var.a;
                _Emissive_Tex_var = tex2D(_Emissive_Tex,TRANSFORM_TEX(_rotate_EmissiveUV_var, _Emissive_Tex));
                float _colorShift_Speed_var = 1.0 - cos(_time_var.g*_ColorShift_Speed);
                float viewShift_var = smoothstep( 0.0, 1.0, max(0,dot(normalDirection,viewDirection)));
                float4 colorShift_Color = lerp(_Emissive_Color, lerp(_Emissive_Color, _ColorShift, _colorShift_Speed_var), _Is_ColorShift);
                float4 viewShift_Color = lerp(_ViewShift, colorShift_Color, viewShift_var);
                float4 emissive_Color = lerp(colorShift_Color, viewShift_Color, _Is_ViewShift);
                emissive = emissive_Color.rgb * _Emissive_Tex_var.rgb * emissiveMask;
#endif
//
                //v.2.0.6: GI_Intensity with Intensity Multiplier Filter
                float3 envLightColor = DecodeLightProbe(normalDirection) < float3(1,1,1) ? DecodeLightProbe(normalDirection) : float3(1,1,1);
                float envLightIntensity = 0.299*envLightColor.r + 0.587*envLightColor.g + 0.114*envLightColor.b <1 ? (0.299*envLightColor.r + 0.587*envLightColor.g + 0.114*envLightColor.b) : 1;
                //Final Composition
                finalColor =  saturate(finalColor) + (envLightColor*envLightIntensity*_GI_Intensity*smoothstep(1,0,envLightIntensity/2)) + emissive;


#elif _IS_PASS_FWDDELTA
                //v.2.0.4.4
                _1st_ShadeColor_Step = saturate(_1st_ShadeColor_Step + _StepOffset);
                _2nd_ShadeColor_Step = saturate(_2nd_ShadeColor_Step + _StepOffset);
                //
                //v.2.0.5: If Added lights is directional, set 0 as _LightIntensity
                float _LightIntensity = lerp(0,(0.299*_LightColor0.r + 0.587*_LightColor0.g + 0.114*_LightColor0.b)*attenuation,_WorldSpaceLightPos0.w) ;
                //v.2.0.5: Filtering the high intensity zone of PointLights
                float3 Set_LightColor = lerp(lightColor,lerp(lightColor,min(lightColor,_LightColor0.rgb*attenuation*_1st_ShadeColor_Step),_WorldSpaceLightPos0.w),_Is_Filter_HiCutPointLightColor);
                //
                float3 Set_BaseColor = lerp( (_MainTex_var.rgb*_BaseColor.rgb*_LightIntensity), ((_MainTex_var.rgb*_BaseColor.rgb)*Set_LightColor), _Is_LightColor_Base );
                //v.2.0.5
                float4 _1st_ShadeMap_var = lerp(tex2D(_1st_ShadeMap,TRANSFORM_TEX(Set_UV0, _1st_ShadeMap)),_MainTex_var,_Use_BaseAs1st);
                float3 _Is_LightColor_1st_Shade_var = lerp( (_1st_ShadeMap_var.rgb*_1st_ShadeColor.rgb*_LightIntensity), ((_1st_ShadeMap_var.rgb*_1st_ShadeColor.rgb)*Set_LightColor), _Is_LightColor_1st_Shade );
                float _HalfLambert_var = 0.5*dot(lerp( i.normalDir, normalDirection, _Is_NormalMapToBase ),lightDirection)+0.5; // Half Lambert
//
////ADD CODE SADAFUMI
//#ifdef _USE_SHRMAP
                //float4 _SHRTex_var = tex2D(_SHRTex, TRANSFORM_TEX(Set_UV0, _SHRTex));
//#endif
////ADD CODE SADAFUMI

//ADD CODE SADAFUMI
#ifdef _USE_SHRMAP
#else
                float4 _ShadingGradeMap_var = tex2Dlod(_ShadingGradeMap, float4(TRANSFORM_TEX(Set_UV0, _ShadingGradeMap), 0.0, _BlurLevelSGM));
#endif
//ADD CODE SADAFUMI
                                //v.2.0.6
                                //Minmimum value is same as the Minimum Feather's value with the Minimum Step's value as threshold.
                float _SystemShadowsLevel_var = (attenuation * 0.5) + 0.5 + _Tweak_SystemShadowsLevel > 0.001 ? (attenuation * 0.5) + 0.5 + _Tweak_SystemShadowsLevel : 0.0001;

                //ADD CODE SADAFUMI
#ifdef _USE_SHRMAP
                float _ShadingGradeMapLevel_var = _SHRTex_var.r < 0.95 ? _SHRTex_var.r + _Tweak_ShadingGradeMapLevel : 1;
#else
                float _ShadingGradeMapLevel_var = _ShadingGradeMap_var.r < 0.95 ? _ShadingGradeMap_var.r + _Tweak_ShadingGradeMapLevel : 1;
#endif
//ADD CODE SADAFUMI
                float Set_ShadingGrade = saturate(_ShadingGradeMapLevel_var)*lerp( _HalfLambert_var, (_HalfLambert_var*saturate(1.0+_Tweak_SystemShadowsLevel)), _Set_SystemShadowsToBase );
                //
                float Set_FinalShadowMask = saturate((1.0 + ( (Set_ShadingGrade - (_1st_ShadeColor_Step-_1st_ShadeColor_Feather)) * (0.0 - 1.0) ) / (_1st_ShadeColor_Step - (_1st_ShadeColor_Step-_1st_ShadeColor_Feather)))); // Base and 1st Shade Mask
                float3 _BaseColor_var = lerp(Set_BaseColor,_Is_LightColor_1st_Shade_var,Set_FinalShadowMask);
                //v.2.0.5
                float4 _2nd_ShadeMap_var = lerp(tex2D(_2nd_ShadeMap,TRANSFORM_TEX(Set_UV0, _2nd_ShadeMap)),_1st_ShadeMap_var,_Use_1stAs2nd);
                float Set_ShadeShadowMask = saturate((1.0 + ( (Set_ShadingGrade - (_2nd_ShadeColor_Step-_2nd_ShadeColor_Feather)) * (0.0 - 1.0) ) / (_2nd_ShadeColor_Step - (_2nd_ShadeColor_Step-_2nd_ShadeColor_Feather)))); // 1st and 2nd Shades Mask
                //Composition: 3 Basic Colors as finalColor
                float3 finalColor = lerp(_BaseColor_var,lerp(_Is_LightColor_1st_Shade_var,lerp( (_2nd_ShadeMap_var.rgb*_2nd_ShadeColor.rgb*_LightIntensity), ((_2nd_ShadeMap_var.rgb*_2nd_ShadeColor.rgb)*Set_LightColor), _Is_LightColor_2nd_Shade ),Set_ShadeShadowMask),Set_FinalShadowMask);

//ADD CODE SADAFUMI
#ifdef _USE_SHRMAP
#else
                float4 _Set_HighColorMask_var = tex2D(_Set_HighColorMask, TRANSFORM_TEX(Set_UV0, _Set_HighColorMask));
#endif
//ADD CODE SADAFUMI

                float _Specular_var = 0.5 * dot(halfDirection, lerp(i.normalDir, normalDirection, _Is_NormalMapToHighColor)) + 0.5; // Specular

//ADD CODE SADAFUMI
#ifdef _USE_SHRMAP
                float _TweakHighColorMask_var = (saturate((_SHRTex_var.g + _Tweak_HighColorMaskLevel)) * lerp((1.0 - step(_Specular_var, (1.0 - pow(_HighColor_Power, 5)))), pow(_Specular_var, exp2(lerp(11, 1, _HighColor_Power))), _Is_SpecularToHighColor));
#else
                float _TweakHighColorMask_var = (saturate((_Set_HighColorMask_var.g + _Tweak_HighColorMaskLevel)) * lerp((1.0 - step(_Specular_var, (1.0 - pow(_HighColor_Power, 5)))), pow(_Specular_var, exp2(lerp(11, 1, _HighColor_Power))), _Is_SpecularToHighColor));
#endif
//ADD CODE SADAFUMI
                //v.2.0.6: Add HighColor if _Is_Filter_HiCutPointLightColor is False
                
                
                
                float4 _HighColor_Tex_var = tex2D(_HighColor_Tex,TRANSFORM_TEX(Set_UV0, _HighColor_Tex));
                float3 _HighColor_var = (lerp( (_HighColor_Tex_var.rgb*_HighColor.rgb), ((_HighColor_Tex_var.rgb*_HighColor.rgb)*Set_LightColor), _Is_LightColor_HighColor )*_TweakHighColorMask_var);
                finalColor = finalColor + lerp(lerp( _HighColor_var, (_HighColor_var*((1.0 - Set_FinalShadowMask)+(Set_FinalShadowMask*_TweakHighColorOnShadow))), _Is_UseTweakHighColorOnShadow ),float3(0,0,0),_Is_Filter_HiCutPointLightColor);
                //

                finalColor = saturate(finalColor);
#endif


//v.2.0.4
#ifdef _IS_PASS_FWDBASE
	            fixed4 finalRGBA = fixed4(finalColor,1);
#elif _IS_PASS_FWDDELTA
	            fixed4 finalRGBA = fixed4(finalColor,0);
#endif
#ifdef _USE_CLIP
#else
                finalRGBA.a = Trans_Alpha;
#endif
        
             //   #ifdef _NORMALS_COLOR
             //       finalRGBA.rgb = i.normalDir.xyz;
             //       #ifdef _CHECK_OUTLINE_NORMAL
             //           finalRGBA.rgb = i.Debug_Outline_Normal.xyz;
             //       #endif
        
             //       ////#ifdef _CHECK_OUTLINE_NORMAL
             //       ////    finalRGBA.rgb = i.Debug_Outline_Normal.xyz;
             //       ////#endif
        
             //       if(_Check_Vertex_Color == 1)
             //       {
             //           finalRGBA.rgb = float3(i.vertexColor.r, 0, 0);    
             //       }
             //       }
             //       else if(_Check_Vertex_Color == 2)
             //       {
             //           finalRGBA.rgb = float3(0, i.vertexColor.g, 0);
             //       }
             //       else if(_Check_Vertex_Color == 3)
             //       {
             //           finalRGBA.rgb = float3(0, 0, i.vertexColor.b);
             //       }
             //       else if(_Check_Vertex_Color == 4)
             //       {
             //           finalRGBA.rgb = i.vertexColor.aaa;    
             //       }
             //   //finalRGBA.rgb = i.Debug_Outline_Normal.rrr;
        	    //#endif
             //   #ifdef _MATCAP_COLOR
             //       finalRGBA.rgb = Set_MatCap;
        	    //#endif    
        
                //finalRGBA.rgb = float3(i.vertexColor.g, i.vertexColor.g, i.vertexColor.g);
                //clip(-1);
                return finalRGBA;
            }
