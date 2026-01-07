Shader "Custom/WheatWind"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _WindStrength ("Wind Strength", Float) = 0.15
        _WindSpeed ("Wind Speed", Float) = 0.8
        _WindScale ("Wind Scale", Float) = 0.5

        _PlayerPushRadius ("Player Push Radius", Float) = 0.6
        _PlayerPushStrength ("Player Push Strength", Float) = 0.2
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalRenderPipeline"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            float4 _MainTex_ST;

            float _WindStrength;
            float _WindSpeed;
            float _WindScale;

            float _PlayerPushRadius;
            float _PlayerPushStrength;

            float3 _PlayerPos;

            // Псевдо-рандом
            float Hash(float n)
            {
                return frac(sin(n) * 43758.5453);
            }

            Varyings vert (Attributes v)
            {
                Varyings o;
                
                float height = saturate(v.uv.y);
                
                float seed = floor(v.positionOS.x * 10.0) + floor(v.positionOS.z * 10.0);
                float randomPhase = Hash(seed) * 6.28318;
                
                float wind =
                    sin(_Time.y * _WindSpeed +
                        v.positionOS.x * _WindScale +
                        randomPhase);
                
                float3 worldPos = TransformObjectToWorld(v.positionOS.xyz);
                float2 toVertex = worldPos.xz - _PlayerPos.xz;
                float dist = length(toVertex);
                float push = smoothstep(_PlayerPushRadius, 0.0, dist);
                float2 dir = normalize(toVertex + 0.0001);
                float2 sideDir = float2(-dir.y, dir.x);
                float bend = push * _PlayerPushStrength * height;
                
                v.positionOS.x += wind * _WindStrength * height;
                v.positionOS.x += sideDir.x * bend;
                v.positionOS.z += sideDir.y * bend;

                o.positionHCS = TransformObjectToHClip(v.positionOS.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            half4 frag (Varyings i) : SV_Target
            {
                return SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
            }

            ENDHLSL
        }
    }
}
