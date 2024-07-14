<template>
    <div class="app-content-header">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm">
                    <h3 class="mb-0">{{ title }}</h3>
                </div>
            </div>
        </div>
    </div>
</template>
<script lang="ts">
export default {
    props: ['title'],
}
</script>