<template>
    
    <div class="my-card" ref="map"></div>

</template>
  
<script>

    export default{
        data(){
            return{
                map: null,
                geocoder: null,
                address: "",
                center: { lat: -34.901113, lng: -56.164531 },
                loading : true,
                markers : [],
                idEmpresa: 0,
                sucursales: [],                 
            }
        },
        beforeMount(){
            const { id } = this.route.params;
            this.idEmpresa = id;
            this.buscarEmpresa();
        },
        mounted(){
            //this.initMap();
        },
        methods: {
           /* initMap() {
                this.map = new window.google.maps.Map(this.$refs.map, {
                    center: this.center,
                    zoom: 10,
                });

            },
            addMarkers() {
                this.sucursales.forEach((sucursal) => {
                    const marker = new window.google.maps.Marker({
                    position: sucursal.ubicacion,
                    map: this.map,
                    title: sucursal.nombre,
                    });

                    marker.addListener('click', () => {
                        this.selectSucursal(sucursal.id);
                    });

                    this.markers.push(marker);
                });
            },
*/
            selectSucursal(id){
                console.log(id);
            },
            
            buscarEmpresa() {
                this.loading = true;
                EmpresasController.getById(this.idEmpresa).then((response) => {
                    const empresa = response.data;
                    console.log(empresa);
                    this.sucursales = empresa.sucursales;
                    if (this.sucursales.length > 0){
                        this.center = JSON.parse(this.sucursales[0].ubicacion.replace(/lat:/g, '"lat":').replace(/lng:/g, '"lng":'));
                        console.log(this.center);
                    }
                    this.loading = false;
                })
                .catch ((error) => {
                console.error('Error al obtener empresa:', error);
                });
            },
        },

    }
</script>

<style scoped>
    .my-card {
        border-radius: 24px !important;
        height: 600px;
    }

</style>