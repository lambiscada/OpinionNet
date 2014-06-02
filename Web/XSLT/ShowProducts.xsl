<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:param name="name" />
  <xsl:param name="price" />
  <xsl:param name="vendedor" />
  <xsl:param name="remainingTime" />
  <xsl:param name="addComment" />
  <xsl:param name="urlAddComment" />
  <xsl:param name="showComment" />
  <xsl:param name="urlShowComment" />
  <xsl:param name="noComments" />
  <xsl:param name="urlVote" />
  <xsl:param name="vote" />
  <xsl:param name="showVotes" />
  <xsl:param name="urlShowPuntuation" />
  <xsl:param name="showPuntuation" />
  <xsl:param name="favorito" />
  <xsl:param name="urlFavorito" />
  
  <xsl:template match="/">   
    <xsl:apply-templates />
  </xsl:template>
  
  <xsl:template match="/Productos">
    <html>
      <table id="ShowProducts" class="info">
        <thead>
          <tr>
            <th>
               <xsl:value-of select="$name" />
            </th>
            <th>
              <xsl:value-of select="$vote" />
            </th>
            <th>
              <xsl:value-of select="$showVotes" />
            </th>
            <th>
              <xsl:value-of select="$price" />
            </th>
            <th>
              <xsl:value-of select="$remainingTime" />
            </th>
            <th>
              <xsl:value-of select="$favorito" />
            </th>
          </tr>
        </thead>

        <tbody>
          <xsl:apply-templates />

        </tbody>
      </table>
      

    </html>

  </xsl:template>

  <xsl:template match="Producto">
    <tr>
      <td>
        <xsl:value-of select="Nombre"/>
      </td>
      <td>
        <xsl:element name="a">
          <xsl:attribute name="href">
            <xsl:value-of select="$urlVote"/>
            <xsl:value-of select="Vendedor"/>
          </xsl:attribute>
          <xsl:value-of select="$vote"/>
        </xsl:element>
      </td>
      <td>
        <xsl:element name="a">
          <xsl:attribute name="href">
            <xsl:value-of select="$urlShowPuntuation"/>
            <xsl:value-of select="Vendedor"/>
          </xsl:attribute>
          <xsl:value-of select="Vendedor"/>
        </xsl:element>
      </td>
      <td>
        <xsl:value-of select="Precio"/> €
      </td>
      <td>
        <xsl:value-of select="Minutos"/>
      </td>
      <td>
        <xsl:element name="a">
          <xsl:attribute name="href">
            <xsl:value-of select="$urlAddComment"/>
            <xsl:value-of select="idProducto"/>
          </xsl:attribute>
          <xsl:value-of select="$addComment"/>
        </xsl:element>
      </td>
      <td>
          <xsl:element name="a">
              <xsl:attribute name="href">
                <xsl:value-of select="$urlShowComment"/>
                <xsl:value-of select="idProducto"/>
              </xsl:attribute>
              <xsl:value-of select="$showComment"/>
            </xsl:element>    
      </td>
      <td>
        <xsl:element name="a">
          <xsl:attribute name="href">
            <xsl:value-of select="$urlFavorito"/>
            <xsl:value-of select="idProducto"/>
          </xsl:attribute>
          <xsl:value-of select="$favorito"/>
        </xsl:element>
      </td>
    </tr>
  
  
  </xsl:template>

  <xsl:template match="ExistMoreProducts">
   
    
  </xsl:template>

</xsl:stylesheet>


