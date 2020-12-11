Shader "Custom/A2"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap ("Normal Map", 2D) = "bump" {}
		_GlossMap("Specular Map", 2D) = "white" {}
		_NoiseMap("Noise Map", 2D) = "white" {}
		_Reflection("Cube Map", CUBE) = "" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
        _MeshDetail ("MeshDetail", float) = 1
		_Alpha ("Alpha", Range(0,1)) = 1
		_Velocidade("Velocidade", float) = 2
		_Altura("Altura", float) = 1
		_Magnitude("Magnitude", float) = 1
		_Periodo("Periodo", float) = 1
		_ForcaNormal("Forca normal", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector"="True"}
        LOD 200
		// Não vai escrever o objeto no z-buffer, já que é transparente.
		ZWrite Off
		// Descrição da forma de fazer o blend da transparencia.
		Blend SrcAlpha OneMinusSrcAlpha

		// Pega a imagem que está atrás do objeto, em screen position. 
		GrabPass{"_GrabTexture"}

        CGPROGRAM
        // Macros para o compilador saber quais são as funções. 
		//Já que estamos usando grab position, não é necessário alpha, (Isso geraria uma refração quando o objeto original ainda está vizivel, 
		// o que não parece correto.) Mas o trabalho pede isso.
        #pragma surface surf Standard addshadow tessellate:tesselation alpha 
		#pragma vertex vert
        #pragma target 4.6

		//Textura principal.
        sampler2D _MainTex;
		//Mapa de normal.
		sampler2D _BumpMap;
		//Mapa de specular.
		sampler2D _GlossMap;
		// Imagem por trás do objeto.
		sampler2D _GrabTexture;
		// Mapa de barulho para distorções.
		sampler2D _NoiseMap;
		// CubMap de reflexão.
		samplerCUBE _Reflection;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_BumpMap;
			float2 uv_GlossMap;
			float3 viewDir;
			float3 worldRefl;
			float3 worldPos;
			float4 screenPos;
			INTERNAL_DATA

        };
		// Valor de cor.
        fixed4 _Color;
		// Valor de specular.
		half _Glossiness;
		//Nível de subdivisões do Tesselation.
		float _MeshDetail;
		//Nível do aplha.
		float _Alpha;
		// Velocidade das ondas.
		float _Velocidade;
		// Altura das ondas.
		float _Altura;
		// Ofsset da distorção.
		float _Magnitude;
		// Periodo da distorção.
		float _Periodo;
		//Forca do mapa de normal;
		float _forcaNormal;

		//Função de tesselação, retorna o número de subdivisões.
		float4 tesselation()
		{
			return _MeshDetail;
		}

		// Função de vertex.
		void vert(inout appdata_full v)
		{
		// Move cada vertex no axis z pelo tempo, baseado na posição implicita pelo texcoord. Poderia usar uma textura como o noise map
		//para conseguir resultados diferentes. 
			v.vertex.z += sin(_Time.y + v.texcoord.x * _Velocidade) * _Altura;
			
		}

		// Função Surface.
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			// Pega a posição do pixel na tela, dada pelo INPUT.
			float4 screenPos = IN.screenPos;

			// Lê o cubeMap usando a reflexão do mundo. No caso do surface shader, esse valor é dado automaticamente pela Unity.
			float4 Reflexao = texCUBE(_Reflection, IN.worldRefl);

			//Lê o mapa de textura que será usado para distorcer a refração da água.
			float2 Distorcao = tex2D(_NoiseMap, IN.worldPos.xy + sin(_Time.w / _Periodo));
			float4 DistorcaoScr = screenPos;
			// Multiplicamos pelo valor da magnitude, para podermos controlar a força da distorção.
			DistorcaoScr.xy += Distorcao * _Magnitude;
			// Lemos o _GrabTexture, que é a imagem por trás do objeto, usando um valor retorcido, que precisa ser projetado, já que está em 
			//screenPos.
			float4 Caustica = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(DistorcaoScr));

			// Pegamos a textura principal e multiplicamos pela cor.
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			// O albedo será a cor multiplicado pela refração e somado com a reflexão do cubMap. (Nesse caso eu multiplico porque fica mais agradavel)
            o.Albedo = Caustica*_Color * Reflexao;
			// Também pode-se usar um mapa de specular. Apesar do resultado não ficar tão interessante.
			o.Smoothness = tex2D (_GlossMap, IN.uv_GlossMap).a * _Glossiness;
			//Abre-se o mapa de normal e o lê.
			o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
			// E finalmente o alpha é aplicado.
            o.Alpha = _Alpha;
        }
        ENDCG
    }
	// Acaba gerando sombras desnecessárias, mas já que o resultado fica mais bonito com do que sem (Gera um deph falso), eu decidi deixar.
    FallBack "Diffuse"
}
