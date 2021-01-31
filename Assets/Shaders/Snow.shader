Shader "P12P/Snow"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _NormalTex ("Normal", 2D) = "bump" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _BloodTex ("Blood splats", 2D) = "white" {}
        _StaticSnowMask ("Static Snow Mask (R)", 2D) = "white" {}
        _DynamicSnowMask ("Dynamic Snow Mask", 2D) = "bump" {}
        _SnowNormalMul ("Snow Normal Multiplier", Range(0, 1)) = 0.5
        _SnowLimit ("Snow Limit", Range(0, 1)) = 0.5
        _SnowHeight ("Snow Height", Range(0, 10)) = 0.0
        _SnowShading ("Snow Shading", Range(0, 1)) = 0.0
        _SnowTessalation ("Snow Resolution", Range(1, 100)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert tessellate:tessFixed

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _NormalTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BloodTex;
            float3 worldPos;
        };

        sampler2D _StaticSnowMask;
        float4 _StaticSnowMask_ST;
        sampler2D _DynamicSnowMask;
        half _SnowHeight;
        half _SnowLimit;
        fixed _SnowShading;

        static const fixed3 SNOW_DIR = fixed3(0,1,0);
        void vert (inout appdata_full v) {
            half2 uv = TRANSFORM_TEX(v.texcoord, _StaticSnowMask);
            half snowPower = max(dot(v.normal, SNOW_DIR), _SnowLimit) * _SnowHeight * tex2Dlod(_StaticSnowMask, fixed4(uv, 0, 1.0)).r * tex2Dlod(_DynamicSnowMask, fixed4(v.texcoord.xy, 0, 1.0)).a;
            v.vertex.y += snowPower;

            half snowNormalOffset = snowPower / _SnowHeight;
            snowNormalOffset = abs(abs(snowNormalOffset * 2 - 1) - 1);
            v.normal.y -= snowNormalOffset * _SnowShading;
            v.normal.xz += snowNormalOffset * _SnowShading;
            v.normal = normalize(v.normal);
        }

        float _SnowTessalation;
        float4 tessFixed()
        {
            return _SnowTessalation;
        }

        half _Glossiness;
        half _Metallic;
        half _SnowNormalMul;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)


        // This needs to be same in script! 
        #define MAX_BLOOD 32
        half4 u_BloodSplats[MAX_BLOOD];
        sampler2D _BloodTex;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;


            [unroll(MAX_BLOOD)]
            for (int i = 0; i < MAX_BLOOD; ++i) {
                float distance = length(IN.worldPos.xz - u_BloodSplats[i].xy);
                if (distance < u_BloodSplats[i].z) {
                    fixed4 blood = tex2D (_BloodTex, 0.5 - (u_BloodSplats[i].xy - IN.worldPos.xz) * u_BloodSplats[i].w);
                    o.Albedo = lerp(o.Albedo, blood.rgb, blood.a * abs(1.0 - distance / u_BloodSplats[i].z));
                }
            }

            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;

            half4 snowNormal = tex2D(_DynamicSnowMask, IN.uv_MainTex);
            half3 normal = UnpackNormal(tex2D(_NormalTex, IN.uv_MainTex));
            o.Normal = lerp(normal, snowNormal * 2.0 - 1.0, snowNormal.a * _SnowNormalMul);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
