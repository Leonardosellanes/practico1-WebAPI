<template>
    <div class=" w-full h-full  flex flex-row justify-between">
        <div class="w-3/4 min-w-3/4">
            <a-space direction="vertical" style="width: 100%;">
                <a-page-header style="border: 1px solid rgb(235, 237, 240)" title="Informacion del producto" />
                <div class="space-y-10">
                    <div class="w-full flex justify-center" v-if="data == null">
                        <a-spin :indicator="indicator" @spinning="true" />
                    </div>
                    <a-space v-else align="start">
                        <a-image :width="400" :height="400" style="object-fit: cover;"
                            :src="'data:image/png;base64,' + data.base64" />
                        <a-divider type="vertical" style="height: 400px;" />
                        <a-space direction="vertical" style="width: 90%;" align="start">
                            <a-tag color="pink">{{ data.categoria.nombre }}</a-tag>
                            <a-descriptions :title="data.titulo" layout="vertical" :bordered="false">
                                <a-descriptions-item label="Descripcion">{{ data.descripcion }}</a-descriptions-item>
                                <a-descriptions-item label="Iva">{{ data.tipo_iva }}</a-descriptions-item>
                                <a-descriptions-item label="Pdf">
                                    <template v-if="data.pdf !== ''">
                                        <a-button type="link" @click="viewPdf">
                                            <PaperClipOutlined />Ver pdf
                                        </a-button>
                                    </template>
                                    <template v-else>
                                        No data
                                    </template>
                                </a-descriptions-item>
                            </a-descriptions>
                        </a-space>
                        <a-card :title="'$' + data.precio" :bordered="false" style="width: 300px">
                            <p>Medios de pago:</p>
                            <p>-Paypal</p>
                            <p>-Ethereum</p>
                            <p>-Tarjeta de credito</p>
                            <template #actions>
                                <a-input-number id="inputNumber" v-model:value="cantidad" :min="1" :max="10" />
                                <a-button type="primary" shape="round" @click="agregarACarrito">
                                    Añadir al carrito
                                </a-button>
                            </template>
                        </a-card>
                    </a-space>
                    <div>
                        <div class="w-full flex items-center justify-between">
                            <p class="mb-2">Opiniones</p>
                            <a-button type="dashed" shape="round" class="mr-2" @click="showModal">
                                Calificar
                            </a-button>
                            <a-modal v-model:open="open" title="Calificar Producto" :confirm-loading="confirmLoading"
                                @ok="handleOpinion" centered>
                                <a-space direction="vertical" style="width: 100%;">
                                    <a-input v-model:value="tituloOpinion" placeholder="Titulo" />
                                    <a-textarea v-model:value="descripcionOpinion" placeholder="Opinion" :rows="4" />
                                    <a-rate v-model:value="stars" :tooltips="desc" />
                                </a-space>
                            </a-modal>
                        </div>
                        <div class="w-6/6 overflow-x-scroll py-4 text-center">
                            <a-space>
                                <template v-if="opiniones.length > 0">
                                    <template v-for="(opinion, index) in opiniones.slice().reverse()" :key="index">
                                        <a-card :title="opinion.titulo" :bordered="false" style="width: 300px" class="mb-4">
                                            <p>{{ opinion.descripcion }}</p>
                                            <template #actions>
                                                <a-rate :value="opinion.estrellas" disabled />
                                            </template>
                                        </a-card>
                                    </template>
                                </template>
                                <template v-else>
                                    <a-empty :image="simpleImage" />
                                </template>
                            </a-space>
                        </div>
                    </div>
                </div>
            </a-space>
        </div>
        <div class="w-1/4 max-w-2/5 overflow-y-scroll flex flex-col items-center justify-start ">
            <div class="flex items-center justify-between mb-2">
                <a-page-header style="border: 1px solid rgb(235, 237, 240)"
                    title="Aqui productos que te pueden interesar" />
            </div>
            <a-space direction="vertical">
                <template v-for="producto in productosAsociados" :key="producto.id">
                    <a-card style="width: 400px; margin-right: 16px;" hoverable @click="cardClicked(producto)">
                        <a-card-meta :title="producto.titulo" :description="'$' + producto.precio.toFixed(2)">
                            <template #avatar>
                                <a-avatar :size="64" :src="'data:image/png;base64,' + producto.base64" />
                            </template>
                        </a-card-meta>
                    </a-card>
                </template>
            </a-space>
        </div>

    </div>
</template>

<script setup>
import { ref, onMounted, onBeforeMount, h } from 'vue';
import ProductosController from '../../services/ProductosController';
import OpinionesController from '../../services/OpinionesController'
import { useRoute } from 'vue-router';
import { LoadingOutlined, PaperClipOutlined } from '@ant-design/icons-vue';
import { Empty, message } from 'ant-design-vue';
import CategoriaController from '../../services/CategoriaController';
import { useRouter, onBeforeRouteUpdate } from 'vue-router';
import CarritoController from '../../services/CarritoController';

const router = useRouter();
const simpleImage = Empty.PRESENTED_IMAGE_SIMPLE;
const route = useRoute();
const productId = ref(null);
const data = ref(null)
const opiniones = ref([])
const productosAsociados = ref([])
const cantidad = ref(1);
const open = ref(false);
const tituloOpinion = ref('');
const descripcionOpinion = ref('');
const stars = ref(0);
const desc = ref(['Terrible', 'Malo', 'Normal', 'Bueno', 'Maravilloso']);
const confirmLoading = ref(false);
const indicator = h(LoadingOutlined, {
    style: {
        fontSize: '24px',
    },
    spin: true,
});

const cargarProducto = () => {
    ProductosController.getProductoById(productId.value)
        .then((response) => {
            data.value = response.data
            opiniones.value = response.data.opinionesAsociadas
            cargarAsociados(response.data.categoriaId)
        })
        .catch((error) => {
            message.error('Error al obtener la lista de categorias:');
        });
}

const cargarAsociados = (catId) => {
    CategoriaController.getById(catId)
        .then((response) => {
            CategoriaController.getById(response.data.categoriaAsociada.id)
                .then((response) => {
                    productosAsociados.value = response.data.productos;
                })
        })
        .catch((error) => {
            message.error('Error al obtener la lista de productos asociados:');
        });
};

const showModal = () => {
    open.value = true;
};
const handleOpinion = () => {
    confirmLoading.value = true;
    const data = {
        id: 0,
        titulo: tituloOpinion.value,
        descripcion: descripcionOpinion.value,
        estrellas: stars.value,
        productoId: productId.value
    }

    OpinionesController.createOpinion(data)
        .then(() => {
            confirmLoading.value = false
            open.value = false
            opiniones.value = [...opiniones.value, data];
            tituloOpinion.value = ''
            descripcionOpinion.value = ''
            stars.value = 0
            message.success(`¡Gracias por tu opinion!`);
        })
        .catch((error) => {
            message.error(`No se pudo registrar la calificacion`);
        });
};

const cardClicked = (item) => {
    productId.value = item.id;
    router.push({ name: 'Product', params: { id: item.id } });
}

const viewPdf = () => {
    if (data.value.base64pdf) {
        const binaryData = atob(data.value.base64pdf);
        const arrayBuffer = new ArrayBuffer(binaryData.length);
        const uint8Array = new Uint8Array(arrayBuffer);

        for (let i = 0; i < binaryData.length; i++) {
            uint8Array[i] = binaryData.charCodeAt(i);
        }

        const blob = new Blob([uint8Array], { type: 'application/pdf' });
        const blobUrl = URL.createObjectURL(blob);

        window.open(blobUrl, '_blank');
    } else {
        console.error('La cadena Base64 del PDF es nula o vacía.');
    }
};

const fetchData = async () => {
    cargarProducto()
};

onBeforeRouteUpdate(() => {
    fetchData();
});

onBeforeMount(() => {
    productId.value = route.params.id;
});

onMounted(() => {
    cargarProducto()
});

const agregarACarrito = () => {
    CarritoController.buscarCarritoActual().then((response) => {
        if (response && response.status === 200) {
            const productos = response.data.carritos;
            if (productos && productos.some(carrito => carrito.productoId === data.value.id)) {
                message.info('El producto ya se encuentra en el carrito');
            } else {
                const orden = response.data.id;
                CarritoController.agregarProducto(orden, data.value.id, cantidad.value).then((response) => {
                    if (response && response.status === 200) {
                        message.success('Producto agregado al carrito');
                    } else {
                        message.error('Error al agregar producto');
                    }
                })
            }
        }
    })

}
</script>
