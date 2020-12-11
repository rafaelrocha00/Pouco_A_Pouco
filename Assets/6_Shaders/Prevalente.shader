Shader "Custom/Prevalente"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Hatch ("Hatch", 2D) = "white" {}
		_HatchColor ("Hatch color", color) = (1,1,1,1)
		_VelocidadeHatch ("Velocidade Hatch", Float) = 1
		_ForcaHatch("Forca Hatch", Range(0,1)) = 1
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_Hatch;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
		sampler2D _Hatch;
		fixed4 _HatchColor;
		float _ForcaHatch;
		float _VelocidadeHatch;


        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
			fixed4 hatch = tex2D(_Hatch,IN.uv_Hatch + (_Time.x * _VelocidadeHatch));
			fixed4 ColorF = (1 - hatch) * _HatchColor;
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb + (ColorF * _ForcaHatch);
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
