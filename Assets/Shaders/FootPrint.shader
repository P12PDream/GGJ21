Shader "P12P/FootPrint"
{
    Properties
    {
        _MainTex ("Mask", 2D) = "white" {}
        _NormalTex ("Normal", 2D) = "bump" {}
        _AlphaMul ("Alpha Multiplier", Range(0, 1)) = 0.5
        _AlphaMin ("Alpha Min", Range(0, 1)) = 0.0
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 100

        ZWrite off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _NormalTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            half _AlphaMul;
            half _AlphaMin;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col;
                col.rgba = tex2D(_NormalTex, i.uv).rgba;
                col.a = tex2D(_MainTex, i.uv).a;

                col.a = max(_AlphaMin * ceil(col.a), col.a) * _AlphaMul;

                clip(col.a - 0.001);

                return col;
            }
            ENDCG
        }
    }
}
