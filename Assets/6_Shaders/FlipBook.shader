Shader "Custom/FlipBook"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_LinhaDesejada("Linha", Float) = 0
        _ColunaDesejada("Coluna", Float) = 0
        _NumeroDeColunas("NumeroDeColunas", Float) = 0
        _NumeroDeLinhas("NumeroDeLinhas", Float) = 0
    
    }
    SubShader
    {
        Tags
	     { 
	         "Queue"="Transparent" 
	         "IgnoreProjector"="True" 
	         "RenderType"="Transparent" 
	         "PreviewType"="Plane"
	         "CanUseSpriteAtlas"="True"
	     }

		 // Configuraçãoes de sprites
	    Cull Off
	    Lighting Off
	    ZWrite Off
	    Fog { Mode Off }
	    Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _Color;
		float _LinhaDesejada;
        float _ColunaDesejada;
        float _NumeroDeColunas;
        float _NumeroDeLinhas;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			float2 Coord  = float2(IN.uv_MainTex.x/_NumeroDeLinhas + (_LinhaDesejada/_NumeroDeLinhas), IN.uv_MainTex.y/_NumeroDeColunas + (_ColunaDesejada/_NumeroDeColunas));
            fixed4 c = tex2D (_MainTex, Coord) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
