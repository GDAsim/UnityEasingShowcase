Shader "SimpleMoveLerp"
{
    Properties
    {
        [MainColor] _BaseColor("Base Color", Color) = (1, 1, 1, 1)

        _Translation("Translation", Vector) = (0,0,0)
        _Rotation("Rotation", Vector) = (0,0,0,0)
        _Scale("Scale", Vector) = (1,1,1)

        _Delta("Delta", Vector) = (0,0,0)

        // Too Long, Unity Inspector Failed
        //[Enum(Linear,0,SineIn,1,SineOut,2,SineInOut,3,QuadIn,4,QuadOut,5,QuadInOut,6,CubicIn,7,CubicOut,8,CubicInOut,9,QuartIn,10,QuartOut,11,QuartInOut,12,QuintIn,13,QuintOut,14,QuintInOut,15,ExpoIn,16,ExpoOut,17,ExpoInOut,18,CircularIn,19,CircularOut,20,CircularInOut,21,BackIn,22,BackOut,23,BackInOut,24,ElasticIn,25,ElasticOut,26,ElasticInOut,27,BounceIn,28,BounceOut,29,BounceInOut,30)] 
        _EasingType ("Easing Type", Int) = 0

        _LerpTime("LerpTime",float) = 0
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            #include "Assets/UnityEasingShowcase/Easing.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
            };

            half4 _BaseColor;

            float3 _Translation;
            float4 _Rotation;
            float4 _Scale;

            float _LerpTime;
            float3 _Delta;
            int _EasingType;

            float4x4 TranslationMatrix(float3 t)
            {
                return float4x4(
                    float4(1, 0, 0, 0),
                    float4(0, 1, 0, 0),
                    float4(0, 0, 1, 0),
                    float4(t.x, t.y, t.z, 1)
                );
            }
            float4x4 ScaleMatrix(float3 s)
            {
                return float4x4(
                    float4(s.x, 0,   0,   0),
                    float4(0,   s.y, 0,   0),
                    float4(0,   0,   s.z, 0),
                    float4(0,   0,   0,   1)
                );
            }
            float4x4 RotationMatrix(float4 q)
            {
                float x2 = q.x + q.x;
                float y2 = q.y + q.y;
                float z2 = q.z + q.z;

                float xx = q.x * x2;
                float yy = q.y * y2;
                float zz = q.z * z2;
                float xy = q.x * y2;
                float xz = q.x * z2;
                float yz = q.y * z2;
                float wx = q.w * x2;
                float wy = q.w * y2;
                float wz = q.w * z2;

                return float4x4(
                    float4(1.0 - (yy + zz), xy + wz,       xz - wy,       0),
                    float4(xy - wz,         1.0 - (xx + zz), yz + wx,     0),
                    float4(xz + wy,         yz - wx,       1.0 - (xx + yy), 0),
                    float4(0, 0, 0, 1)
                );
            }

            float4x4 BuildTRS(float3 pos, float4 rot, float3 scale)
            {
                float4x4 T = TranslationMatrix(pos);
                float4x4 S = ScaleMatrix(scale);
                float4x4 R = RotationMatrix(rot);

                return transpose(mul(mul(S, R), T));
            }

            float ApplyEasing(int type,float t)
            {
                switch (type)
                {
                    case 0:  return Linear(t);
                    case 1:  return SineIn(t);
                    case 2:  return SineOut(t);
                    case 3:  return SineInOut(t);
                    case 4:  return QuadIn(t);
                    case 5:  return QuadOut(t);
                    case 6:  return QuadInOut(t);
                    case 7:  return CubicIn(t);
                    case 8:  return CubicOut(t);
                    case 9:  return CubicInOut(t);
                    case 10: return QuartIn(t);
                    case 11: return QuartOut(t);
                    case 12: return QuartInOut(t);
                    case 13: return QuintIn(t);
                    case 14: return QuintOut(t);
                    case 15: return QuintInOut(t);
                    case 16: return ExpoIn(t);
                    case 17: return ExpoOut(t);
                    case 18: return ExpoInOut(t);
                    case 19: return CircularIn(t);
                    case 20: return CircularOut(t);
                    case 21: return CircularInOut(t);
                    case 22: return BackIn(t);
                    case 23: return BackOut(t);
                    case 24: return BackInOut(t);
                    case 25: return ElasticIn(t);
                    case 26: return ElasticOut(t);
                    case 27: return ElasticInOut(t);
                    case 28: return BounceIn(t);
                    case 29: return BounceOut2(t);
                    case 30: return BounceInOut(t);
                    default: return t;
                }
            }

            Varyings vert(Attributes IN)
            {
                Varyings OUT;

                float t = ApplyEasing(_EasingType, _LerpTime);
                float3 pos = _Translation + _Delta * t;

                float4x4 model = BuildTRS(pos,_Rotation,_Scale);
                OUT.positionHCS = mul(UNITY_MATRIX_VP, mul(model, IN.positionOS));

                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                half4 color = _BaseColor;
                return color;
            }
            ENDHLSL
        }
    }
}
